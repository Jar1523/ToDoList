using System.Diagnostics;

namespace ToDoList.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }


        public virtual ICollection<TaskList> TaskLists { get; set; }
    }
}
