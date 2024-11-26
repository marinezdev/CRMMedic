using System;
using CRM.Models.Models;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Repository
{
    public class MenuRepositorio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<Menu> Seleccionar()
        {
            string consulta = "SELECT id, idjquery, ruta, icono, nombre, idconfiguracion, disponible FROM menu";
            b.ExecuteCommandQuery(consulta);
            List<Menu> resultado = new List<Menu>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Menu item = new Menu();
                item.Id = int.Parse(reader["id"].ToString());
                item.IdJquery = reader["idjquery"].ToString();
                item.Ruta = reader["ruta"].ToString();
                item.Icono = reader["icono"].ToString();
                item.Nombre = reader["nombre"].ToString();
                item.IdConfiguracion = int.Parse(reader["idconfiguracion"].ToString());
                item.Disponible = bool.Parse(reader["disponible"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<MenuMenuRoles> SeleccionarMenuRol()
        {
            string consulta = "SELECT mr.id, mn.nombre AS OpcionMenu, rl.nombre AS Rol, mr.accesible " +
            "FROM menuroles mr " +
            "INNER JOIN menu mn ON mn.id = mr.idmenu " +
            "INNER JOIN roles rl ON rl.id = mr.idrol";            
            b.ExecuteCommandQuery(consulta);
            List<MenuMenuRoles> resultado = new List<MenuMenuRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                MenuMenuRoles item = new MenuMenuRoles();
                item.Menu.Id = int.Parse(reader["id"].ToString());
                item.Menu.Nombre = reader["opcionmenu"].ToString();
                item.Menu.IdJquery = reader["rol"].ToString();
                item.Menu.Disponible = bool.Parse(reader["accesible"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<MenuMenuRoles> SeleccionarMenuPorIdRol(string idrol, string idconfiguracion)
        {
            string consulta = "SELECT mn.idjquery, mn.ruta, mn.icono, mn.nombre " +
            "FROM menuroles mr " +
            "INNER JOIN menu mn ON mn.id = mr.idmenu " +
            "WHERE mn.idconfiguracion = @idconfiguracion " +
            "AND mr.idrol = @idrol " +
            "AND mn.disponible=1 " +
            "AND mr.accesible=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idconfiguracion", idconfiguracion, SqlDbType.Int);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            List<MenuMenuRoles> resultado = new List<MenuMenuRoles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                MenuMenuRoles item = new MenuMenuRoles();
                item.Menu.IdJquery = reader["idjquery"].ToString();
                item.Menu.Ruta = reader["ruta"].ToString();
                item.Menu.Icono = reader["icono"].ToString();
                item.Menu.Nombre = reader["nombre"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected Menu SeleccionarMenuRolPorId(string id)
        {
            string consulta = "SELECT mr.id, mr.idmenu, mn.nombre AS OpcionMenu, rl.id AS IdRol, rl.nombre AS Rol, mr.accesible " +
            "FROM menuroles mr " +
            "INNER JOIN menu mn ON mn.id = mr.idmenu " +
            "INNER JOIN roles rl ON rl.id = mr.idrol " +
            "WHERE mr.id = @id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            Menu resultado = new Menu();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.IdConfiguracion = int.Parse(reader["idmenu"].ToString());
                resultado.Nombre = reader["opcionmenu"].ToString();
                resultado.IdJquery = reader["idrol"].ToString();
                resultado.Icono = reader["rol"].ToString();
                resultado.Disponible = bool.Parse(reader["accesible"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected Menu SeleccionarPorId(string id)
        {
            b.ExecuteCommandQuery("SELECT id, idjquery, ruta, icono, nombre, idconfiguracion, disponible FROM menu WHERE id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            Menu resultado = new Menu();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.IdJquery = reader["idjquery"].ToString();
                resultado.Ruta = reader["ruta"].ToString();
                resultado.Icono = reader["icono"].ToString();
                resultado.Nombre = reader["nombre"].ToString();
                resultado.IdConfiguracion = int.Parse(reader["idconfiguracion"].ToString());
                resultado.Disponible = bool.Parse(reader["disponible"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(Menu items)
        {
            string consulta = "INSERT INTO menu (idjquery,ruta,icono,nombre,idconfiguracion,disponible) VALUES(@idjquery,@ruta,@icono,@nombre,@idconfiguracion,@disponible)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idjquery", items.IdJquery, SqlDbType.NVarChar);
            b.AddParameter("@ruta", items.Ruta, SqlDbType.NVarChar);
            b.AddParameter("@icono", items.Icono, SqlDbType.NVarChar);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", items.IdConfiguracion, SqlDbType.Int);
            b.AddParameter("@disponible", items.Disponible == true ? 1 : 0, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }

        protected int Modificar(Menu items)
        {
            string consulta = "UPDATE menu SET idjquery=@idjquery,ruta=@ruta,icono=@icono,nombre=@nombre,idconfiguracion=@idconfiguracion,disponible=@disponible WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idjquery", items.IdJquery, SqlDbType.NVarChar);
            b.AddParameter("@ruta", items.Ruta, SqlDbType.NVarChar);
            b.AddParameter("@icono", items.Icono, SqlDbType.NVarChar);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@idconfiguracion", items.IdConfiguracion, SqlDbType.Int);
            b.AddParameter("@disponible", items.Disponible, SqlDbType.Bit);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int AgregarMenuRol(MenuRoles items)
        {
            string consulta = "INSERT INTO menuroles (idmenu,idrol, accesible) VALUES(@idmenu,@idrol,@accesible)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idmenu", items.IdMenu, SqlDbType.Int);
            b.AddParameter("@idrol", items.IdRol, SqlDbType.Int);
            b.AddParameter("@accesible", items.Accesible, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }
                       
        protected int ModificarMenuRol(MenuRoles items)
        {
            string consulta = "UPDATE menuroles SET idmenu=@idmenu, idrol=@idrol, accesible=@accesible WHERE id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idmenu", items.IdMenu, SqlDbType.Int);
            b.AddParameter("@idrol", items.IdRol, SqlDbType.Int);
            b.AddParameter("@accesible", items.Accesible, SqlDbType.Bit);
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }



    }
}
