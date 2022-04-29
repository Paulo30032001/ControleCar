
namespace ControleCar.Services.Util
{
    public class Criptografia
    {
        public static readonly string HASH_SALT = "009305dac362fcf6ba249498c6994c56";

        public static string sha1(string senha)
        {
            senha = HASH_SALT + senha;

            var senhaBytes = System.Text.Encoding.ASCII.GetBytes(senha);
            var hashBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(senhaBytes);
            var sbHash = new System.Text.StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes)
            {
                // can be "x2" if you want lowercase
                sbHash.Append(b.ToString("x2"));
            }

            return sbHash.ToString();
        }





    }
}
