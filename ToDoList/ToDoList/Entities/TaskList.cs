namespace ToDoList.Entities
{
    public class TaskList
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
