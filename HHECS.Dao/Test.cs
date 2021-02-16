using Dapper;
using HHECS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Dao
{
    public class Test
    {
        public static void Main(String[] args)
        {
            IDbConnection connection = new SqlConnection("Data Source=.;Initial Catalog=lijinji;Persist Security Info=True;User ID=sa;Password=123456");
            //var result = connection.Execute("Insert into [user](userName,password) values(@UserName,@Password)",new {UserName = "liufu",Password="123456"});

            //var userList = Enumerable.Range(0, 10).Select(i => new
            //{
            //    UserName = "liufu" + i,
            //    Password = "12345"
            //});
            //var result2 = connection.Execute("Insert into [user](userName,password) values(@UserName,@Password)", userList);

            //var result = connection.Query<User>("select * from [user]");
            //var a = connection.Get<User>(3);
            //var b = connection.Delete<User>(4);
            //var c = connection.GetList<User>();
            //var d = connection.GetList<User>(new { UserName = "liufu" });
            //var f = connection.GetList<User>("where userName like 'liufu%'" );
            //var g = connection.GetListPagedAsync<User>(2, 3,"","id").ContinueWith(t=> {
            //    Console.WriteLine("g");
            //});

            //var lookup = new Dictionary<string, User>();
            //IDbConnection connection2 = new SqlConnection("Data Source=.;Initial Catalog=lijinji;Persist Security Info=True;User ID=sa;Password=123456");
            //connection2.Query<User, Role, User>("select u.*,rl.* from [user] u join userRole r on u.id = r.userId join [role] rl on r.roleId = rl.id;", (user, role) =>
            //   {
            //       List<Role> roles = new List<Role>();
            //       User u;
            //       if (!lookup.TryGetValue(user.Id.ToString(), out u))
            //       {
            //           u = user;
            //           lookup.Add(u.Id.ToString(), u);
            //       }
            //       u.Roles.Add(role);
            //       return u;
            //   });
            //var list = lookup.Values.ToList();
            //Console.WriteLine(list);
            TaskExcuteStatus temp = (TaskExcuteStatus)(Convert.ToInt32("111"));


            Console.Read();

        }
    }
}
