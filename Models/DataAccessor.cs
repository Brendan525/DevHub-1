using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.IO;

namespace DevHub.Models
{
    public class DataAccessor
    {
        private static string GetConnectionString()
        {
            try
            {
                StreamReader reader = new StreamReader(@"C:\temp\db4server.txt");
                string server = reader.ReadLine();
                reader.Close();
                return server;
            }
            catch
            {
                return "Server=; Database = DevHub; user id = devhub; password=pass1;";
               
            }
        }

        private static IDbConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public static List<Question> GetAllQuestions()
        {
            IDbConnection db = GetConnection();
            return db.GetAll<Question>().ToList();            
        }

        public static List<Question> GetRecentQuestions(int count = 3)
        {
            string query = $"SELECT TOP ({count}) * FROM Question ORDER BY posted DESC";
            IDbConnection db = GetConnection();
            return db.Query<Question>(query).ToList();
        }

        public static List<Question> SearchQuestions(string column, string search)
        {
            string query = $"SELECT * FROM Question WHERE {column} like '%{search}%'";
            IDbConnection db = GetConnection();
            return db.Query<Question>(query).ToList();
        }

        public static void SaveQuestion(Question toSave)
        {

            IDbConnection db = GetConnection();

            if (toSave.id > 0)
            {
                db.Update(toSave);
            }
            else
            {
                toSave.id = db.Insert(toSave);
            }
        }

        public static void DeleteQuestion(long id)
        {
            IDbConnection db = GetConnection();

            db.Delete(new Question() { id = id});
        }

        public static Question GetQuestion(long id)
        {
            IDbConnection db = GetConnection();
            return db.Get<Question>(id);
        }

        public static List<Answer> GetAllAnswers()
        {
            IDbConnection db = GetConnection();
            return db.GetAll<Answer>().ToList();
        }

        public static void SaveAnswer(Answer toSave)
        {

            IDbConnection db = GetConnection();

            if (toSave.id > 0)
            {
                db.Update(toSave);
            }
            else
            {
                toSave.id = db.Insert(toSave);
            }
        }

        public static void DeleteAnswer(long id)
        {
            IDbConnection db = GetConnection();

            db.Delete(new Answer() { id = id });
        }

        public static Answer GetAnswer(long id)
        {
            IDbConnection db = GetConnection();
            return db.Get<Answer>(id);
        }

        public static List<Answer> GetQuestionAnswers(long question_id)
        {
            string query = $"SELECT * FROM answer WHERE question_id = {question_id} ORDER BY upvote DESC";
            IDbConnection db = GetConnection();
            return db.Query<Answer>(query).ToList();
        }

        public static List<string> GetCategory()
        {
            string query = $"SELECT DISTINCT category FROM question";
            IDbConnection db = GetConnection();
            return db.Query<string>(query).ToList();
        }

        public static List<string> GetTags()
        {
            List<string> tags = new List<string>();
            List<Question> questions = GetAllQuestions();

            
            foreach(Question q in questions)
            {
                if (q.tags != null && q.tags != "") {

                    string[] tagsArray = q.tags.Split("#");

                    foreach (string tag in tagsArray)
                    {
                        if (!tags.Contains(tag) && tag != "")
                        {
                            tags.Add(tag);
                        }
                    }

                }
            }
            return tags;
        }
    }
}
