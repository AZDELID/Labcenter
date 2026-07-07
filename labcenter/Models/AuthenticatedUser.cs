namespace labcenter.Models
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public int Id { get; private set; }
        public string Nombre { get; private set; }
    }
}
