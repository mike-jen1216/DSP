using Microsoft.AspNetCore.Mvc;

public class SingletonControllers : Controller
{
    public IActionResult GetSingleton()
    {
        return Ok(SingletonDTO.Instance); 
    }
}
