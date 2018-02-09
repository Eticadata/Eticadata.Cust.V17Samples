using Eticadata.ERP;
using System;

namespace Eticadata.Cust.DeskTop.Helpers
{
    public static class DatabaseCust
    {

        public static EtiConnNetBD GetConnection(EtiAplicacao eti,string SQLInstance,string login,string password, string database)
        {
            EtiConnNetBD conn = new EtiConnNetBD(eti);
            try
            {
                conn.MakeConnectionToServer(SQLInstance, login, password, database, 0);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return conn;
        }
    }
}
