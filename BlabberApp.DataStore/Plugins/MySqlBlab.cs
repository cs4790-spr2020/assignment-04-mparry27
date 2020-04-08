using System;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;
using BlabberApp.DataStore.Interfaces;
using BlabberApp.Domain.Entities;
using BlabberApp.Domain.Interfaces;

namespace BlabberApp.DataStore.Plugins
{
    public class MySqlBlab : IBlabPlugin
    {
        private MySqlConnection dcBlab;
        public MySqlBlab()
        {
            this.dcBlab = new MySqlConnection("server=142.93.114.73;database=mparry27;user=mparry27;password=letmein");
            try
            {
                this.dcBlab.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public void Close()
        {
            this.dcBlab.Close();
        }
        public void Create(IEntity obj)
        {
            Blab blab = (Blab)obj;
            try
            {
                DateTime now = DateTime.Now;
                string sql = "INSERT INTO blabs (sys_id, message, dttm_created, user_id) VALUES ('"
                     + blab.Id + "', '"
                     + blab.Message + "', '"
                     + now.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                     + blab.User.Email + "')";
                MySqlCommand cmd = new MySqlCommand(sql, this.dcBlab);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public IEnumerable ReadAll()
        {
            try
            {
               string sql = "SELECT * FROM blabs";
                MySqlDataAdapter daBlabs = new MySqlDataAdapter(sql, this.dcBlab); // To avoid SQL injection.
                MySqlCommandBuilder cbBlabs = new MySqlCommandBuilder(daBlabs);
                DataSet dsBlabs = new DataSet();

                daBlabs.Fill(dsBlabs);

                ArrayList allBlabs = new ArrayList();

                foreach( DataRow dtRow in dsBlabs.Tables[0].Rows)
                {
                    allBlabs.Add(dtRow);
                }
                
                return allBlabs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public IEntity ReadById(Guid Id)
        {
            try
            {
                string sql = "SELECT * FROM blabs WHERE blabs.sys_id = '" + Id.ToString() + "'";
                MySqlDataAdapter daBlab = new MySqlDataAdapter(sql, this.dcBlab); // To avoid SQL injection.
                MySqlCommandBuilder cbBlab = new MySqlCommandBuilder(daBlab);
                DataSet dsBlab = new DataSet();

                daBlab.Fill(dsBlab, "blabs");
                
                ArrayList idBlabs = new ArrayList();

                foreach(DataRow row in dsBlab.Tables[0].Rows)
                {
                    idBlabs.Add(row);
                }
                return (IEntity)idBlabs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public IEnumerable ReadByUserId(string email)
        {
            try
            {
                string sql = "SELECT * FROM blabs WHERE blabs.user_id = '" + email.ToString() + "'";
                MySqlDataAdapter daBlabs = new MySqlDataAdapter(sql, this.dcBlab); // To avoid SQL injection.
                MySqlCommandBuilder cbBlabs = new MySqlCommandBuilder(daBlabs);
                DataSet dsBlabs = new DataSet();

                daBlabs.Fill(dsBlabs);

                ArrayList userBlabs = new ArrayList();

                foreach( DataRow dtRow in dsBlabs.Tables[0].Rows)
                {
                    userBlabs.Add(dtRow);
                }
                
                return userBlabs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Update(IEntity obj)
        {
            Blab blab = (Blab)obj;
        }

        public void Delete(IEntity obj)
        {
            Blab blab = (Blab)obj;
            try{
                string sql = "DELETE FROM blabs WHERE id='"+blab.Id+"'";
                MySqlCommand cmd = new MySqlCommand(sql, dcBlab);
                cmd.ExecuteNonQuery();
            } catch(Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
    }
}