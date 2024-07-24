namespace ToDoList.Entities
{
    public class TaskList
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
