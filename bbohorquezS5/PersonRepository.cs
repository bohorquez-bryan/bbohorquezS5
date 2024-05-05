using bbohorquezS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bbohorquezS5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();

        }
        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");
                Persona persona = new() { Name = name };
                result = conn.Insert(persona);
                StatusMessage = string.Format("Se inserto una persona", result, name);

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al insertar una persona", name, ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            {
                try
                {
                    Init();
                    return conn.Table<Persona>().ToList();

                }
                catch (Exception ex)
                {
                    StatusMessage = string.Format("Error al insertar una persona", ex.Message);
                }
            }
            return new List<Persona>();
        }


        public void EditPerson(int id, string newName)
        {
            try
            {
                
                if (string.IsNullOrEmpty(newName))
                    StatusMessage = string.Format("El nuevo nombre es requerido");

                var personaUpdate = conn.Table<Persona>().FirstOrDefault(p => p.Id == id);
                if (personaUpdate != null)
                {
                    personaUpdate.Name = newName;
                    int result = conn.Update(personaUpdate);
                    StatusMessage = string.Format("Se actualizó la persona: {0}", newName);
                }
                else
                {
                    StatusMessage = string.Format("No se encontro a la persona");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar la persona, el nombre es requerido", ex.Message);
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                var personaDelete = conn.Table<Persona>().FirstOrDefault(p => p.Id == id);
                if (personaDelete != null)
                {
                    int result = conn.Delete(personaDelete);
                    StatusMessage = string.Format("Se eliminó la persona correctamente");
                }
                else
                {
                    StatusMessage = string.Format("No se encontró la persona a eliminar");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar la persona: {ex.Message}");
            }
        }
    }

}
