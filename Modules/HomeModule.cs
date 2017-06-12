using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace ToDoList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      }; //homepage with links to view all tasks and all categories
      Get["/tasks"] = _ => {
        List<Task> AllTasks = Task.GetAll();
        return View["tasks.cshtml", AllTasks];
      }; //page view all tasks
      Get["/categories"] = _ => {
        List<Category> AllCategories = Category.GetAll();
        return View["categories.cshtml", AllCategories];
      }; //page view all categories
      Get["/categories/new"] = _ => {
        return View["categories_form.cshtml"];
      };
      Get["/tasks/new"] = _ => {
        return View["tasks_form.cshtml"];
      }; //returns form to add new task
      Post["/tasks/new"] = _ => {
        Task newTask = new Task(Request.Form["task-description"], Request.Form["category-id"]);
        newTask.Save();
        List<Task> AllTasks = Task.GetAll();
        return View["tasks.cshtml", AllTasks];
      }; //posts from form adding new task
      Get["/categories/new"] = _ => {
        return View["categories_form.cshtml"];
      }; //returns form to add new task
      Post["/categories/new"] = _ => {
        Category newCategory = new Category(Request.Form["category-name"]);
        newCategory.Save();
        List<Category> AllCategories = Category.GetAll();
        return View["categories.cshtml", AllCategories];
      }; //posts from form adding new category
      Get["tasks/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Task SelectedTask = Task.Find(parameters.id);
        List<Category> TaskCategories = SelectedTask.GetCategories();
        List<Category> AllCategories = Category.GetAll();
        model.Add("task", SelectedTask);
        model.Add("taskCategories", TaskCategories);
        model.Add("allCategories", AllCategories);
        model.Add("isCompleted", SelectedTask);
        return View["task.cshtml", model];
      }; //returns individual instance of task
      Get["/categories/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category SelectedCategory = Category.Find(parameters.id);
        List<Task> CategoryTasks = SelectedCategory.GetTasks();
        List<Task> AllTasks = Task.GetAll();
        model.Add("category", SelectedCategory);
        model.Add("categoryTasks", CategoryTasks);
        model.Add("allTasks", AllTasks);
        return View["category.cshtml", model];
      }; //returns individual instance of category
      Post["task/add_category"] = _ => {
        Category category = Category.Find(Request.Form["category-id"]);
        Task task = Task.Find(Request.Form["task-id"]);
        task.AddCategory(category);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Category> TaskCategories = task.GetCategories();
        List<Category> AllCategories = Category.GetAll();
        model.Add("task", task);
        model.Add("taskCategories", TaskCategories);
        model.Add("allCategories", AllCategories);
        model.Add("isCompleted", task);
        return View["task.cshtml", model];
      }; //posts from form adding category
      Post["category/add_task"] = _ => {
        Category category = Category.Find(Request.Form["category-id"]);
        Task task = Task.Find(Request.Form["task-id"]);
        category.AddTask(task);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Category> TaskCategories = task.GetCategories();
        List<Category> AllCategories = Category.GetAll();
        model.Add("task", task);
        model.Add("taskCategories", TaskCategories);
        model.Add("allCategories", AllCategories);
        return View["category.cshtml", model];
      }; //posts from form adding task to category page
      Get["/task/{id}/completed"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Task SelectedTask = Task.Find(parameters.id);
        string taskCompleted = Request.Query["task-completed"];
        model.Add("form-type", taskCompleted);
        model.Add("task", SelectedTask);
        return View["completed.cshtml", model];
      }; //returns page confirming task completion

      Patch["/task/{id}/completed"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Task SelectedTask = Task.Find(parameters.id);
        List<Category> TaskCategories = SelectedTask.GetCategories();
        List<Category> AllCategories = Category.GetAll();
        SelectedTask.Update(true);
        model.Add("task", SelectedTask);
        model.Add("taskCategories", TaskCategories);
        model.Add("allCategories", AllCategories);
        model.Add("isCompleted", SelectedTask);
        return View["task.cshtml", model];
      }; //returns category page with task marked completed

      Get["category/edit/{id}"] = parameters => {
        Category SelectedCategory = Category.Find(parameters.id);
        return View["category_edit.cshtml", SelectedCategory];
      }; //edit individual category
      Patch["category/edit/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category SelectedCategory = Category.Find(parameters.id);
        SelectedCategory.Update(Request.Form["category-name"]);
        List<Task> CategoryTasks = SelectedCategory.GetTasks();
        List<Task> AllTasks = Task.GetAll();
        model.Add("category", SelectedCategory);
        model.Add("categoryTasks", CategoryTasks);
        model.Add("allTasks", AllTasks);
        return View["category.cshtml", model];
      }; //posts from editing individual category
      Get["category/delete/{id}"] = parameters => {
        Category SelectedCategory = Category.Find(parameters.id);
        return View["category_delete.cshtml", SelectedCategory];
      }; //delete individual category
      Delete["category/delete/{id}"] = parameters => {
        Category SelectedCategory = Category.Find(parameters.id);
        SelectedCategory.Delete();
        List<Category> AllCategories = Category.GetAll();
        return View["categories.cshtml", AllCategories];
      }; //delete individual category
    }
  }
}
