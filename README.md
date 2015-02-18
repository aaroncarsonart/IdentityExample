IdentityExample
===============
An example of how to use the ASP.NET MVC 5 EF 6 Identity property properly.

Important Links
===============
This is all based on the tutorial available here:

http://blogs.msdn.com/b/webdev/archive/2013/10/20/building-a-simple-todo-application-with-asp-net-identity-and-associating-users-with-todoes.aspx

You can view the working website here:

http://identityexample.azurewebsites.net/

Short Tutorial
==============
All of this information is located in the above linked tutorial, but it requires some digging to get through. I've reproduced a shortened version that relates to this specific project. It should be enough to get you going. For more in-depth information, please go read through their full tutorial. 

Please note that you will need to add additional `using` statements at the top of the various files that we are creating/editing. They will not be mentioned here, but Visual Studio will correct these issues if you right click the section of code that is having issues.

1. Create your desired model. In this project, my model's name is Review. In your model field definitions (`IdentityExample/Models/Review.cs`), add the following code:

  `public virtual ApplicationUser User { get; set; }`
2. Go to the Identity model (`IdentityExample/Models/IdentityModel.cs`) and add the following code just below the `GenerateUserIdentityAsync` method:

  `public virtual ICollection<Review> review { get; set; }`
  
  Make sure that you replace the Review model here with the actual model name you are using in your project.
3. Add a model controller to your project's `Controllers` folder. Choose ApplicationDBContext as your database in the dialog box that pops up and select the model that we're working with.
4. Now we're going to add some code that integrates the identity model. Just above the controller declaration (`public class ReviewsController : Controller {` in this case), add the following code:
  ```
  private ApplicationDbContext db;
  private UserManager<ApplicationUser> manager;

  public ReviewsController()
  {
    db = new ApplicationDbContext();
    manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
  }
  ```
5. At this point, the program is ready to go with the identity stuff. We need to change the controller so that it adds the user ID to the Review (or whatever your model name is) table. In the create section of the controller, add `var currentUser = manager.FindById(User.Identity.GetUserId());` just above the if statement. Within that same statement, add `review.User = manager.FindById(User.Identity.GetUserId());` just above the rest of the code in that statement. The resulting code should look something like the following:
  ```
  public async Task<ActionResult> Create([Bind(Include = "ReviewID,Content,ReviewDate,Score")] Review review)
  {
    var currentUser = manager.FindById(User.Identity.GetUserId());
  
    if (ModelState.IsValid)
    {
      review.User = manager.FindById(User.Identity.GetUserId());
      db.Reviews.Add(review);
      await db.SaveChangesAsync();
      return RedirectToAction("Index");
    }
  
    return View(review);
  }
  ```
When a user is logged in and tries to create a review (or an instance of whatever model you are using), it will take the user's ID and add it into the user ID section of the model automatically. If a user is not logged in, a null value will be inserted.
6. To finalize everything, `[authorize]` tags should be added above the edit, delete, and create sections of the controllers. This will force the user to be logged in in order to access those parts of the controller. Other tags can be added as well to allow certain users (specific users, user roles, etc.) to access those parts of the controller. These tags can be found in the main tutorial linked up at the top.
