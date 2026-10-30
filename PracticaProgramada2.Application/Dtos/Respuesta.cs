namespace PracticaProgramada2.Application.Dtos
{
    public class Respuesta<T>
    {
        public bool EsCorrecto { get; set; }

        public string Mensaje { get; set; } = string.Empty;

        public int Codigo { get; set; }

        public T Dato { get; set; } = default!;

        public Respuesta()
        {
            EsCorrecto = true;
            Mensaje = "Operación realizada correctamente";
            Codigo = 200;
        }
    }
}
