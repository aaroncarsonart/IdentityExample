IdentityExample
===============
An example of how to use the ASP.NET MVC 5 EF 6 Identity property properly.

This is all based on the tutorial available here:

http://blogs.msdn.com/b/webdev/archive/2013/10/20/building-a-simple-todo-application-with-asp-net-identity-and-associating-users-with-todoes.aspx

Short Tutorial (Incomplete)
===========================
All of this information is located in the above linked tutorial, but it requires some digging to get through. I've reproduced a shortened version that relates to this specific project. For more in-depth information, please go read through their full tutorial.

1. Create your desired model. In this project, my model's name is Review. In your model field definitions (`IdentityExample/Models/Review.cs`), add the following code:

  `public virtual ApplicationUser User { get; set; }`
2. Go to the Identity model `(IdentityExample/Models/IdentityModel.cs)` and add the following code just below the `GenerateUserIdentityAsync` method:

  `public virtual ICollection<Review> review { get; set; }`
  
  Make sure that you replace the Review model here with the actual model name you are using in your project.
3. Add a model controller to your project's `Controllers` folder. Choose ApplicationDBContext as your database in the dialog box that pops up and select the model that we're working with.
4. We're going to have to add quite a bit of code to the controller folder. Just above the controller declaration (`public class ReviewsController : Controller {` in this case), add the following code:

  ```
  private ApplicationDbContext db;
  private UserManager<ApplicationUser> manager;

  public ReviewsController()
  {
    db = new ApplicationDbContext();
    manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
  }
  ```
5. More Steps to Come
