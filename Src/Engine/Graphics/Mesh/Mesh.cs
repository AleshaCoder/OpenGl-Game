using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Mesh
{
    public class Mesh
    {
        private readonly int _vao;

        private readonly int _vbo;
        private readonly int _tcbo;
        private readonly int _nbo;
        private readonly int _ibo;
        private int _size = 0;

        private Vector3[] _positions = new Vector3[0];
        private Vector2[] _texCoords = new Vector2[0];
        private Vector3[] _normals = new Vector3[0];
        private int[] _indices = new int[0];

        public Mesh()
        {
            _vbo = GL.GenBuffer();
            _tcbo = GL.GenBuffer();
            _nbo = GL.GenBuffer();
            _ibo = GL.GenBuffer();
            _vao = GL.GenVertexArray();
        }

        public void AddVertices(Vector3[] positions, Vector2[] texCoords, Vector3[] normals, int[] indices)
        {
            _size = +indices.Length;

            Vector3[] newArray = new Vector3[_positions.Length + positions.Length];
            Array.Copy(_positions, newArray, 0);
            Array.Copy(positions, 0, newArray, _positions.Length, positions.Length);
            _positions = newArray;

            Vector2[] newArray0 = new Vector2[_texCoords.Length + texCoords.Length];
            Array.Copy(_texCoords, newArray0, 0);
            Array.Copy(texCoords, 0, newArray0, _texCoords.Length, texCoords.Length);
            _texCoords = newArray0;

            Vector3[] newArray1 = new Vector3[_normals.Length + normals.Length];
            Array.Copy(_normals, newArray1, 0);
            Array.Copy(normals, 0, newArray1, _normals.Length, normals.Length);
            _normals = newArray1;

            int[] newArray2 = new int[_indices.Length + indices.Length];
            Array.Copy(_indices, newArray2, 0);
            Array.Copy(indices, 0, newArray2, _indices.Length, indices.Length);
            _indices = newArray2;
        }

        public void Complete()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(_positions.Length * Vector3.SizeInBytes), _positions, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _tcbo);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(_texCoords.Length * Vector2.SizeInBytes), _texCoords, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _nbo);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(_normals.Length * Vector3.SizeInBytes), _normals, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ibo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * _indices.Length), _indices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindVertexArray(_vao);

            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);

            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _tcbo);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, true, Vector2.SizeInBytes, 0);

            GL.EnableVertexAttribArray(2);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _nbo);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ibo);

            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.BindVertexArray(_vao);
            GL.DrawElements(PrimitiveType.Triangles, _size, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
