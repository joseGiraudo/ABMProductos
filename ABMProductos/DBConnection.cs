using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data; // aca estan los dataTable y dataReader

namespace ABMProductos
{
    public class DBConnection
    {
        // static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30";

        //Conexión al servidor local con autenticación de Windows
        //private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TUPPI;Integrated Security=True";
        
        //Conexión al servidor de la facultad con autenticación de SQL Sever
        private string connectionString = @"Data Source=172.16.10.196;Initial Catalog=Informatica;User ID=alumno1w1;Password=alumno1w1";
        
        private SqlConnection connection;
        
        private SqlCommand command;
        private SqlDataReader reader;

        // hago una propiedad publica del DataReader porque no puede trabajar desconectado
        public SqlDataReader Reader { get { return reader; } set { reader = value; } }

        // constructor
        public DBConnection()
        {
            connection = new SqlConnection(connectionString);
        }

        // metodos

        private void Conectar()
        {
            connection.Open();
        }
        public void Desconectar()
        {
            connection.Close();
        }
                
        public DataTable ConsultarBD(string SQLquery)
        {
            DataTable table = new DataTable(); // creo el dataTable

            Conectar();
            command = new SqlCommand(SQLquery, connection); // creo el comando con la consulta y la conexion
            
            table.Load(command.ExecuteReader());  // cargo los datos en el dataTable

            Desconectar();

            return table;
        }

        public void LeerTabla(string nombreTabla)
        {
            // con el nombre de la tabla como parametro se carga un DataReader con los datos de la BD
            Conectar();
            string query = "SELECT * FROM " + nombreTabla;
            command = new SqlCommand(query, connection); // creo el comando con la consulta y la conexion

            reader = command.ExecuteReader();

        }

            
    }
}
