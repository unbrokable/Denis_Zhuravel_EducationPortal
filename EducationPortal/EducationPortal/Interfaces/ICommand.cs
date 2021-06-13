using EducationPortal.Models;

namespace EducationPortal.Interfaces
{
    public interface ICommand<T>
    {
        T Execute(int idUser);
    }

    interface ICommandMaterial
    {
        MaterialViewModel Execute(string name, string location, int idUser);
    }
}
