using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class DbContextDatabaseExtensions
    {


        /// <summary>
        /// DataTable扩展方法：将DataTable类型转化为指定类型的实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dateTimeToString">是否需要将日期转换为字符串，默认为转换,值为true</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt, bool dateTimeToString=true) where T : class, new()
        {
            List<T> list = new List<T>();

            if (dt != null)
            {
                List<PropertyInfo> infos = new List<PropertyInfo>();

                Array.ForEach<PropertyInfo>(typeof(T).GetProperties(), p =>
                {
                    if (dt.Columns.Contains(p.Name) == true)
                    {
                        infos.Add(p);
                    }
                });//获取类型的属性集合

                SetList<T>(list, infos, dt, dateTimeToString);
            }

            return list;
        }

        private static void SetList<T>(List<T> list, List<PropertyInfo> infos, DataTable dt, bool dateTimeToString) where T : class, new()
        {
            foreach (DataRow dr in dt.Rows)
            {
                T model = new T();

                infos.ForEach(p =>
                {
                    if (dr[p.Name] != DBNull.Value)//判断属性在不为空
                    {
                        object tempValue = dr[p.Name];
                        if (dr[p.Name].GetType() == typeof(DateTime) && dateTimeToString == true)//判断是否为时间
                        {
                            tempValue = dr[p.Name].ToString();
                        }
                        try
                        {
                            p.SetValue(model, tempValue, null);//设置
                        }
                        catch { }
                    }
                });
                list.Add(model);
            }
        }

        public static DataSet GetPageReocrdByProc(this Database dbcontext, QueryParam model)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@TableName", SqlDbType.VarChar,1000),
                    new SqlParameter("@ReturnFields", SqlDbType.VarChar,8000),
                    new SqlParameter("@PageSize", SqlDbType.Int,4),
                    new SqlParameter("@PageIndex", SqlDbType.Int,4),
                    new SqlParameter("@Where", SqlDbType.VarChar,8000),
                    new SqlParameter("@Orderfld", SqlDbType.VarChar,500),
                    new SqlParameter("@TotalRecord", SqlDbType.Int,4),
                    new SqlParameter("@TotalPage", SqlDbType.Int,4),
                    new SqlParameter("@Sql", SqlDbType.VarChar,8000)
            };
            parameters[0].Value = model.TableName;
            parameters[1].Value = model.ReturnFields;
            parameters[2].Value = model.PageSize;
            parameters[3].Value = model.PageIndex;
            if (model.Where == default(string) || model.Where.Trim() == "")
                parameters[4].Value = "1=1";
            else
                parameters[4].Value = model.Where;
            parameters[5].Value = model.Orderfld;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;

            //调用分页存储过程
            SqlConnection conn = new SqlConnection(dbcontext.Connection.ConnectionString);
            DataSet ds = new DataSet();
            conn.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("Pro_StoredProcedurePage", conn);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
            }


            sqlDA.SelectCommand = command;
            sqlDA.Fill(ds);
            conn.Close();
            string a = parameters[6].Value.ToString();
            model.TotalRecord = int.Parse(parameters[6].Value.ToString());
            model.TotalPage = int.Parse(parameters[7].Value.ToString());
            model.Sql = parameters[8].Value.ToString();
            return ds;

        }
    }
}
