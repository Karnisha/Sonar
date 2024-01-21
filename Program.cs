// See https://aka.ms/new-console-template for more information
//using MySql.Data.MySqlClient;
abstract class DataConnection
{
    
    public abstract void AddData(MySqlConnection conn);
    public abstract void ReadData(MySqlConnection conn);
    public abstract void UpdateData(MySqlConnection conn);
    public abstract void DeleteData(MySqlConnection conn);
}

class TvshowroomDatabase : DataConnection
{

    public override void AddData(MySqlConnection conn)
    {
        conn.Open();
        Console.Write("Enter Tv Brand name: ");
        string? brand_name = Console.ReadLine();

        Console.Write("Enter Tv Model name: ");
        string? model_name = Console.ReadLine();
        Console.Write("Enter the Tv price : ");
        int? tv_price = Convert.ToInt32(Console.ReadLine());

        string insertQuery = $"insert into tvshowroom(Tv_brandname,Tv_modelname,Tv_price) values('{brand_name}' , '{model_name}' ,'{tv_price} ')";
        MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
        cmd.ExecuteNonQuery();
        conn.Close();

    }
    public override void ReadData(MySqlConnection conn)
    {
        conn.Open();
        string sql = "SELECT * FROM tvshowroom";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(reader[0] + " -- " + reader[1] + "--" + reader[2] + " -- " + reader[3]);
        }
        reader.Close();
        conn.Close();
    }
    public override void UpdateData(MySqlConnection conn)
    {
        conn.Open();
        Console.WriteLine("Updating  values into the tables: ");
        Console.WriteLine("What details you want to update");
        Console.WriteLine("1.TV brand name");
        Console.WriteLine("2. TV model name");
        Console.WriteLine("3. TV price");
        string? updatechoice = Console.ReadLine();
        string? updatedata = null;
        switch (updatechoice)
        {
            case "1":
                updatedata = "Tv_brandname";
                Console.Write("Enter the Tv brand name: ");
                break;
            case "2":
                updatedata = "Tv_modelname";
                Console.Write("Enter the Tv model name: ");
                break;
            case "3":
                updatedata = "Tv_price";
                Console.Write("Enter the Tv price : ");
                break;

        }
        string? updatevalue = Console.ReadLine();
        Console.Write("\nEnter id : ");
        string? id = Console.ReadLine();
        string insertQuery = $"UPDATE tvshowroom SET {updatedata}='{updatevalue}' where id ={id}";
        MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
        MySqlDataReader reader = cmd.ExecuteReader();
        conn.Close();

    }
    public override void DeleteData(MySqlConnection conn)
    {
        conn.Open();
        Console.Write("\n Enter Brand name : ");
        string brand_name = Console.ReadLine()!;
        string insertQuery = $"delete from tvshowroom where Tv_brandname='{brand_name}'";
        MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
        cmd.ExecuteNonQuery();

        conn.Close();
    }
}
class Tvshowroom
{
    static void Main(string[] args)
    {
        string server = "server=127.0.0.1;user=root;database=tvshowroommanagement;port=3306;password=Password@12345";
        MySqlConnection conn = new MySqlConnection(server);
        TvshowroomDatabase TvDatabase = new TvshowroomDatabase();
        bool condition=true;
        do 
        {
            Console.WriteLine("Tv Showroom Management System");
            Console.WriteLine("*******************************");
            Console.WriteLine("1. Add TV showroom details");
            Console.WriteLine("2. View TV Showroom details");
            Console.WriteLine("3. Update TV showroom details");
            Console.WriteLine("4. Delete TV showroom details");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your number: ");
            string? num = Console.ReadLine();
            switch (num)
            {
                case "1":
                    TvDatabase.AddData(conn);
                    Console.WriteLine("****Insert the data successfully****");
                    break;
                case "2":
                    TvDatabase.ReadData(conn);
                    Console.WriteLine("****Read  data successfully****");
                    break;
                case "3":
                    TvDatabase.UpdateData(conn);
                    Console.WriteLine("****Update successfully");
                    break;
                case "4":
                    TvDatabase.DeleteData(conn);
                    Console.WriteLine("***Delete data successfully***");
                    break;
                case "5":
                    condition=false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }while(condition);
    }
}

