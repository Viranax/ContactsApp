using System.IO;
using Newtonsoft.Json;

namespace ContactsApp
{
    /// <summary>
    /// Класс реализует метод для сохранения объекта «Проект» в файл и метод загрузки проекта из файла.
    /// Сохранение и загрузка осуществляются в один и тот же файл.
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// Путь по которому сохраняется файл.
        /// </summary>
        private static readonly string path = @"C:\Users\Leeoo\Desktop\ContactsApp.notes";

        /// <summary>
        /// Метод сериализации данных.
        /// </summary>
        /// <param name="data">Данные для сериализации.</param>
        public void SaveToFile(Project data)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                //Вызываем сериализацию и передаем объект, который хотим сериализовать.
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// Метод сериализации данных.
        /// </summary>
        public Project DeserializeProject()
        {
            Project project;

            JsonSerializer serializer = new JsonSerializer();

            using (StreamReader sr = new StreamReader(path))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                project = serializer.Deserialize<Project>(reader);
            }

            return project;
        }
    }
}