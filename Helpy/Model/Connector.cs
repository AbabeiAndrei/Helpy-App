using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using Helpy.Classes;
using MySql.Data.MySqlClient;

namespace Helpy.Model
{
    public class Connector : IDisposable
    {
        private MySqlConnection connection;

        public Connector(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
        }

        private int OpenConnection()
        {
            try
            {
                connection.Open();
                return 0;
            }
            catch (MySqlException ex)
            {
                return ex.Number != 0 ? ex.Number : -1;
            }
            catch
            {
                return -1;
            }
        }

        private int CloseConnection()
        {

            try
            {
                connection.Close();
                return 0;
            }
            catch (MySqlException ex)
            {
                return ex.Number != 0 ? ex.Number : -1;
            }
            catch
            {
                return -1;
            }
        }

        public IEnumerable<Child> GetChilds(int start, int limit)
        {
            try
            {
                string query = "SELECT c.*, SUM(IFNULL(d.amount, 0)) AS total " +
                               "FROM childs AS c " +
                               "LEFT JOIN donations AS d ON d.child_id = c.id " +
                               "GROUP BY d.child_id " +
                               $"LIMIT {start}, {limit}";

                if (OpenConnection() != 0)
                    yield break;

                using (var mcmd = new MySqlCommand(query, connection))
                    using (var mdr = mcmd.ExecuteReader())
                    {
                        while (mdr.Read())
                            yield return new Child
                            {
                                Id = mdr.GetInt32("id"),
                                FullName = mdr.GetString("full_name"),
                                Photo = mdr.GetString("photo"),
                                Address = mdr.GetString("address"),
                                BirthDate = !(mdr["birth"] is DBNull) ? mdr.GetDateTime("birth") : (DateTime?)null,
                                ShortStory = mdr.GetString("short_desc"),
                                Story = mdr.GetString("story"),
                                Amount = mdr.GetDecimal("amount"),
                                From = mdr.GetDateTime("from"),
                                To = mdr.GetDateTime("until"),
                                Top = !(mdr["birth"] is DBNull) && mdr.GetInt16("top") == 1,
                                Deleted = !(mdr["deleted"] is DBNull) && mdr.GetInt16("deleted") == 1,
                                Total = mdr.GetDecimal("total")
                            };

                        mdr.Close();
                    }
            }
            finally
            {
                CloseConnection();
            }
        }

        public int CountChilds()
        {
            try
            {
                string query = "SELECT COUNT(c.id) AS total " +
                               "FROM childs AS c " +
                               "WHERE c.deleted = 0";

                if (OpenConnection() != 0)
                    return 0;

                using (var mcmd = new MySqlCommand(query, connection))
                using (var mdr = mcmd.ExecuteReader())
                {
                    mdr.Read();

                    var mc = mdr.GetInt32("total");

                    mdr.Close();

                    return mc;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public void AddDonor(Donor donor)
        {
            try
            {
                string mquery = "INSERT INTO `donations` (`user_id`, `child_id`, `name`, `address`, `email`, `phone`, `description`, `message`, `amount`, `card_details`, `date`) " +
                                "VALUES (@user_id, @child_id, @name, @address, @email, @phone, @desc, @message, @amount, @card, @date);";


                if (OpenConnection() != 0)
                    return;

                using (var mcmd = new MySqlCommand(mquery, connection))
                {
                    mcmd.Parameters.AddWithValue("user_id", donor.UserId ?? 0);
                    mcmd.Parameters.AddWithValue("child_id", donor.ChildId);
                    mcmd.Parameters.AddWithValue("name", donor.Name ?? string.Empty);
                    mcmd.Parameters.AddWithValue("address", donor.Address ?? string.Empty);
                    mcmd.Parameters.AddWithValue("email", donor.Email ?? string.Empty);
                    mcmd.Parameters.AddWithValue("phone", donor.Phone ?? string.Empty);
                    mcmd.Parameters.AddWithValue("desc", string.Empty);
                    mcmd.Parameters.AddWithValue("message", donor.Message ?? string.Empty);
                    mcmd.Parameters.AddWithValue("amount", donor.Amount);
                    mcmd.Parameters.AddWithValue("card", donor.CardDetails ?? string.Empty);
                    mcmd.Parameters.AddWithValue("date", donor.Date);
                    mcmd.ExecuteNonQuery();
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public void AddChild(Child child)
        {
            try
            {
                string mquery = "INSERT INTO `childs` (`full_name`, `address`, `birth`, `story`, `short_desc`, `amount`, `until`, user_id) " +
                                "VALUES (@name, @address, @birth, @story, @short_desc, @amount, @until, @user_id);";


                if (OpenConnection() != 0)
                    return;

                using (var mcmd = new MySqlCommand(mquery, connection))
                {
                    mcmd.Parameters.AddWithValue("name", child.FullName);
                    mcmd.Parameters.AddWithValue("address", child.Address);
                    mcmd.Parameters.AddWithValue("birth", child.BirthDate);
                    mcmd.Parameters.AddWithValue("story", child.Story);
                    mcmd.Parameters.AddWithValue("short_desc", child.ShortStory);
                    mcmd.Parameters.AddWithValue("amount", child.Amount);
                    mcmd.Parameters.AddWithValue("until", child.To);
                    mcmd.Parameters.AddWithValue("user_id", child.UserId);
                    mcmd.ExecuteNonQuery();
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public int AddUser(User user)
        {
            try
            {
                string mquery = "INSERT INTO `users` (`login`, `pass`, `full_name`, `mail`, `phone`, `address`, cnp) " +
                                "VALUES (@login, @pass, @full_name, @mail, @phone, @address, @cnp);";


                if (OpenConnection() != 0)
                    throw new Exception("Cannot open db connection");

                using (var mcmd = new MySqlCommand(mquery, connection))
                {
                    mcmd.Parameters.AddWithValue("login", user.Name ?? string.Empty);
                    mcmd.Parameters.AddWithValue("pass", user.Pass ?? string.Empty);
                    mcmd.Parameters.AddWithValue("full_name", user.FullName ?? string.Empty);
                    mcmd.Parameters.AddWithValue("mail", user.Mail ?? string.Empty);
                    mcmd.Parameters.AddWithValue("phone", user.Phone ?? string.Empty);
                    mcmd.Parameters.AddWithValue("address", user.Address ?? string.Empty);
                    mcmd.Parameters.AddWithValue("cnp", user.Cnp ?? string.Empty);
                    mcmd.ExecuteNonQuery();
                }

                var mu = GetUserByName(user.Name, false);

                if (mu != null)
                    return mu.Id;

                throw new Exception("User added but not found...");//some weird shit happened here...
            }
            finally
            {
                CloseConnection();
            }
        }

        public User GetUserByName(string name, bool openConnection = true)
        {
            try
            {
                string query = "SELECT * FROM users WHERE login = @name";

                if (openConnection && OpenConnection() != 0)
                    return null;

                User mu = null;

                using (var mcmd = new MySqlCommand(query, connection))
                {
                    mcmd.Parameters.AddWithValue("name", name);

                    using (var mdr = mcmd.ExecuteReader())
                    {
                        if (mdr.Read())
                        {
                            mu = new User
                            {
                                Id = mdr.GetInt32("id"),
                                Name = mdr.GetString("login"),
                                Pass = mdr.GetString("pass"),
                                FullName = mdr.GetString("full_name"),
                                Mail = mdr.GetString("mail"),
                                Phone = mdr.GetString("phone"),
                                Cnp = mdr.GetString("cnp"),
                                Address = mdr.GetString("address")
                            };
                        }

                        mdr.Close();
                    }
                }

                return mu;
            }
            finally
            {
                if(openConnection)
                    CloseConnection();
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                string query = "SELECT * FROM users WHERE id = @id";

                if (OpenConnection() != 0)
                    return null;

                User mu = null;

                using (var mcmd = new MySqlCommand(query, connection))
                {
                    mcmd.Parameters.AddWithValue("id", id);

                    using (var mdr = mcmd.ExecuteReader())
                    {
                        if (mdr.Read())
                        {
                            mu = new User
                            {
                                Id = mdr.GetInt32("id"),
                                Name = mdr.GetString("login"),
                                Pass = mdr.GetString("pass"),
                                FullName = mdr.GetString("full_name"),
                                Mail = mdr.GetString("mail"),
                                Phone = mdr.GetString("phone"),
                                Cnp = mdr.GetString("cnp"),
                                Address = mdr.GetString("address")
                            };
                        }

                        mdr.Close();
                    }
                }

                return mu;
            }
            finally
            {
                CloseConnection();
            }
        }
        
        public IEnumerable<Donor> GetDonors(int limit, DateTime? date = null)
        {
            try
            {
                string query = "SELECT d.*, c.full_name child_name " +
                               "FROM donations d " +
                               "INNER JOIN childs c ON c.id = d.child_id " +
                               (date.HasValue ? "WHERE DATE(d.`date`) = @date " : string.Empty) +
                               "ORDER BY d.amount " ;

                if (OpenConnection() != 0)
                    yield break;

                using (var mcmd = new MySqlCommand(query, connection))
                {
                    if (date.HasValue)
                        mcmd.Parameters.AddWithValue("date", date.Value.Date);

                    using (var mdr = mcmd.ExecuteReader())
                    {
                        while (mdr.Read())
                            if(limit-- > 0)
                                yield return new Donor
                                {
                                    Id = mdr.GetInt32("id"),
                                    UserId = mdr.GetInt32("user_id"),
                                    ChildId = mdr.GetInt32("child_id"),
                                    ChildName = mdr.GetString("child_name"),
                                    Name  = mdr.GetString("name"),
                                    Address = mdr.GetString("address"),
                                    Email =  mdr.GetString("email"),
                                    Phone = mdr.GetString("phone"),
                                    Message = mdr.GetString("message"),
                                    Amount = mdr.GetDecimal("amount"),
                                    CardDetails = mdr.GetString("card_details"),
                                    Date = mdr.GetDateTime("date")
                                };
                            else
                                yield break;

                        mdr.Close();
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            if(connection != null)
            {
                try
                {
                    connection.Close();
                }
                catch 
                {
                    //nothing to do
                }
                connection.Dispose();
            }
        }

        #endregion
    }
}