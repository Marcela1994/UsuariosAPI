using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Configuration;

namespace UsuariosApi.DAO
{
    public class UsuariosDAO
    {
        private readonly string _connectionString;

        public UsuariosDAO(IConfiguration iConfig)
        {
            this._connectionString = iConfig.GetSection("ConnectionStrings").GetSection("ConnectionString").Value;
            Console.WriteLine("Cadena de conexion " + this._connectionString);
        }

        public List<PerfilUsuario> consultarUsuarios() {
            List<PerfilUsuario> listadoUsuarios = new List<PerfilUsuario>();
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("consultaUsuarios", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Console.WriteLine("Consulta del usuario");
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            sql.Open();
                            cmd.ExecuteNonQuery();
                            adapter.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                adapter.Fill(dt);
                                for (int i = 0; i <= dt.Rows.Count; i++) {
                                    PerfilUsuario usuario = new PerfilUsuario();
                                    Rol rol = new Rol();
                                    Dependencias dep = new Dependencias();
                                    usuario.id = int.Parse(dt.Rows[i]["id"].ToString());
                                    usuario.documento = (dt.Rows[i]["documento"].ToString());
                                    usuario.username = (dt.Rows[i]["username"].ToString());
                                    usuario.nombre = (dt.Rows[i]["nombre"].ToString());
                                    usuario.mail = (dt.Rows[i]["email"].ToString());
                                    usuario.estado = Boolean.Parse(dt.Rows[i]["estado"].ToString());
                                    // Completar informacion dependencia
                                    dep.id = int.Parse(dt.Rows[i]["id_dependencia"].ToString());
                                    dep.codigo = (dt.Rows[i]["codigo"].ToString());
                                    dep.descrípcion = (dt.Rows[i]["descripcion"].ToString());
                                    dep.cargo = (dt.Rows[i]["cargo"].ToString());
                                    dep.estado = Boolean.Parse(dt.Rows[i]["estado_dependencia"].ToString());
                                    // Completar informacion rol
                                    rol.id = int.Parse(dt.Rows[i]["id_rol"].ToString());
                                    rol.descrípcion = (dt.Rows[i]["descripcion_rol"].ToString());
                                    rol.siglaRol = (dt.Rows[i]["sigla_rol"].ToString());
                                    rol.estado = Boolean.Parse(dt.Rows[i]["estado_rol"].ToString());

                                    usuario.dep = dep;
                                    usuario.rol = rol;

                                    listadoUsuarios.Add(usuario);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al insertar");
                Console.WriteLine("-> " + e.Message);
                Console.WriteLine("-> " + e.ToString());
                return listadoUsuarios;
            }

            return listadoUsuarios;
        }

        public int crearUsuario(Usuarios usuario)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("insertarUsuario", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", usuario.id));
                        cmd.Parameters.Add(new SqlParameter("@documento", usuario.documento));
                        cmd.Parameters.Add(new SqlParameter("@username", usuario.username));
                        cmd.Parameters.Add(new SqlParameter("@nombre", usuario.nombre));
                        cmd.Parameters.Add(new SqlParameter("@email", usuario.mail));
                        cmd.Parameters.Add(new SqlParameter("@estado", usuario.estado));
                        cmd.Parameters.Add(new SqlParameter("@id_rol", usuario.idRol));
                        cmd.Parameters.Add(new SqlParameter("@id_dep", usuario.idDependencia));
                        sql.Open();
                        int res = cmd.ExecuteNonQuery();
                        Console.WriteLine("Se inserto correctamente");
                        return res;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al insertar");
                Console.WriteLine("-> " + e.Message);
                Console.WriteLine("-> " + e.ToString());
                return 0;
            }
        }

        public List<PerfilUsuario> consultarUsuarioPorID(int id)
        {
            List<PerfilUsuario> listadoUsuarios = new List<PerfilUsuario>();
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("consultaUsuarioPorID", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        Console.WriteLine("Consulta del usuario");
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            sql.Open();
                            cmd.ExecuteNonQuery();
                            adapter.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                adapter.Fill(dt);
                                PerfilUsuario usuario = new PerfilUsuario();
                                Rol rol = new Rol();
                                Dependencias dep = new Dependencias();
                                usuario.id = int.Parse(dt.Rows[0]["id"].ToString());
                                usuario.documento = (dt.Rows[0]["documento"].ToString());
                                usuario.username = (dt.Rows[0]["username"].ToString());
                                usuario.nombre = (dt.Rows[0]["nombre"].ToString());
                                usuario.mail = (dt.Rows[0]["email"].ToString());
                                usuario.estado = Boolean.Parse(dt.Rows[0]["estado"].ToString());
                                // Completar informacion dependencia
                                dep.id = int.Parse(dt.Rows[0]["id_dependencia"].ToString());
                                dep.codigo = (dt.Rows[0]["codigo"].ToString());
                                dep.descrípcion = (dt.Rows[0]["descripcion"].ToString());
                                dep.cargo = (dt.Rows[0]["cargo"].ToString());
                                dep.estado = Boolean.Parse(dt.Rows[0]["estado_dependencia"].ToString());
                                // Completar informacion rol
                                rol.id = int.Parse(dt.Rows[0]["id_rol"].ToString());
                                rol.descrípcion = (dt.Rows[0]["descripcion_rol"].ToString());
                                rol.siglaRol = (dt.Rows[0]["sigla_rol"].ToString());
                                rol.estado = Boolean.Parse(dt.Rows[0]["estado_rol"].ToString());

                                usuario.dep = dep;
                                usuario.rol = rol;

                                listadoUsuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al consultar");
                Console.WriteLine("-> " + e.Message);
                Console.WriteLine("-> " + e.ToString());
                return listadoUsuarios;
            }

            return listadoUsuarios;
        }



        public int eliminarUsuario(Usuarios usuario)
        {
            return 0;
        }


        public int modificarUsuario(int id, Usuarios usuario)
        {
            List<PerfilUsuario> listadoUsuarios = new List<PerfilUsuario>();
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                        using (SqlCommand cmd = new SqlCommand("modificarUsuario", sql))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@id", id));
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@documento", usuario.documento));
                            cmd.Parameters.Add(new SqlParameter("@username", usuario.username));
                            cmd.Parameters.Add(new SqlParameter("@nombre", usuario.nombre));
                            cmd.Parameters.Add(new SqlParameter("@email", usuario.mail));
                            cmd.Parameters.Add(new SqlParameter("@estado", usuario.estado));
                            sql.Open();
                            int res = cmd.ExecuteNonQuery();
                            Console.WriteLine("Se inserto correctamente");
                            return res;
                        }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al editar");
                Console.WriteLine("-> " + e.Message);
                Console.WriteLine("-> " + e.ToString());
                return 0;
            }
        }

        public int eliminarUsuario(int id)
        {
            List<PerfilUsuario> listadoUsuarios = new List<PerfilUsuario>();
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("eliminarUsuario", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sql.Open();
                        int res = cmd.ExecuteNonQuery();
                        Console.WriteLine("Se elimino correctamente");
                        return res;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al editar");
                Console.WriteLine("-> " + e.Message);
                Console.WriteLine("-> " + e.ToString());
                return 1;
            }
        }
    }
}
