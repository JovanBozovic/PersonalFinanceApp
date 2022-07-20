using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Npgsql;
using PersonalFinanceApp.Database;
using PersonalFinanceApp.Database.Repositories;
using PersonalFinanceApp.Services;
using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Transactions;

namespace PersonalFinanceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // string csv_file_path = @"C:\Users\Instructor\Desktop\Projekat\PersonalFinanceApp\PersonalFinanceApp\Files\transactions.csv";
            // DataTable csvTransactionsData = GetDataTableFromCSVFile(csv_file_path);

            // var transactions = csvTransactionsData.AsEnumerable().Select(row => new Transaction 
            // {
            //     Id = row.Field<int>("id"),
            //     Beneficiary_Name = row.Field<string>("beneficiary-name"),
            //     Date = row.Field<DateTime>("date"),
            //     Direction = row.Field<string>("direction"),
            //     Amount = row.Field<float>("amount"),
            //     Description = row.Field<string>("description"),
            //     Currency = row.Field<string>("currency"),
            //     Mcc = row.Field<int>("mcc"),
            //     Kind = row.Field<string>("kind"),
            // });
            // id,beneficiary-name,date,direction,amount,description,currency,mcc,kind
            // Console.WriteLine("Rows count:" + csvData.Rows.Count);    
            // Console.ReadLine();


            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddDbContext<TransactionsDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("FinanceDb"));
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            InitializeDatabase(app);

            app.MapControllers();

            app.Run();
        }

        private static DataTable GetDataTableFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();

            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();

                    foreach (string column in colFields)
                    {
                        DataColumn datacolumn = new DataColumn(column);
                        datacolumn.AllowDBNull = true;
                        csvData.Columns.Add(datacolumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }

                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return csvData;
        }

        private static void InitializeDatabase(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope();

                scope.ServiceProvider.GetRequiredService<TransactionsDbContext>().Database.Migrate();
            }
        }

        private static string CreateConnectionString(IConfiguration configuration)
        {
            var username = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? configuration["Database:Username"];
            var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? configuration["Database:Password"];
            var database = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? configuration["Database:Name"];
            var host = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? configuration["Database:Host"];
            var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? configuration["Database:Port"];
            Console.WriteLine("Proba" + port);
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = int.Parse(port),
                Database = database,
                Username = username,
                Password = password,
                Pooling = true,
            };
            Console.WriteLine("Proba : " + builder.ConnectionString);
            return builder.ConnectionString;
        }
    }
}