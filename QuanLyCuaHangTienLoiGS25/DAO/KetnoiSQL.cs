using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangTienLoiGS25.DAO
{
    internal class KetnoiSQL
    {
        
        private string connectionString = @"Data Source=LAPTOP-QB932246\SQLEXPRESS;
        Initial Catalog=QuanLyCHTLGS25;Integrated Security=True";
        public SqlConnection GetConnect()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public int ExecuteNonQuery(string query, object[] parameters = null)
        {
            int data = 0;
            using (SqlConnection connection = GetConnect())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    string[] parameterList = query.Split(' ');
                    int i = 0;
                    foreach (var item in parameterList)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameters[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                connection.Open();
            }
            return data;
        }

        public DataTable ExecuteQuery(string query, object[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = GetConnect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    string[] parameterList = query.Split(' ');
                    int i = 0;
                    foreach (var item in parameterList)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameters[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try
                {
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ ở đây, ví dụ như ghi log, thông báo lỗi, hoặc thực hiện các hành động khác
                    Console.WriteLine("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
                }
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ ở đây, ví dụ như ghi log, thông báo lỗi, hoặc thực hiện các hành động khác
                    Console.WriteLine("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
                }
            }
            return dt;
        }

        public DataTable thongtinChucVu()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select MaCV, TenCV from ChucVu", GetConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable thongtinNhanVien()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from NhanVien", GetConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable thongtinKhachHang()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from KhachHang", GetConnect());
            adapter.Fill(data);
            return data;
        }

        public DataTable thongtinSanPham()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from SanPham", GetConnect());
            adapter.Fill(data);
            return data;
        }

        public DataTable thongtinNCC()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from NhaCungCap", GetConnect());
            adapter.Fill(data);
            return data;
        }
    }
}
