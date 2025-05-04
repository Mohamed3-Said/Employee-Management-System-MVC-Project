using DemoBusinessLogic.Data_Transfer_Objects;
using DemoBusinessLogic.Services.Interfaces;
using DemoMVCPresentation.ViewModels.DepartmentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVCPresentation.Controllers
{
    public class DepartmentsController(IDepartmentService _departmentService , 
                ILogger<DepartmentsController> _logger ,
                IWebHostEnvironment _environment) : Controller

    {
        //BaseURL/Controller/Action.
        [HttpGet]
        public IActionResult Index()
        {
          //  ViewData["Message"] = new DepartmentDto() { Name = "Test Of ViewData" };
           // ViewBag.Message = new DepartmentDto() { Name = "Test of View Bag" };
            var department=_departmentService.GetAllDepartments();
            return View(department);
        }

        #region 1-Create Department
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public IActionResult Create() => View();
        
        //[Create]http post:
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid) //Server Side Validation.
            {
                try
                {
                    int Result = _departmentService.AddDepartment(departmentDto);
                    string Message;

                     if (Result > 0)
                    {
                        Message = $"Department{departmentDto.Name} is Created Successfully";
                    }
                    else 
                    { 

                        Message = $"Department{departmentDto.Name} Can Not Be Created";
                    }
                    TempData["Message"]=Message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex) 
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }

                }
            }
            return View(departmentDto);
        }
        #endregion

        #region 2-Details Department
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); //400
            var department = _departmentService.GetDepartmentById(id.Value);
            if(department is null) return NotFound(); //404 
            return View(department);
        }
        #endregion

        #region 3-Edit Department
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var department=_departmentService.GetDepartmentById(id.Value);
            if(department is null) return NotFound();
            var departmentViewModel = new DepartmentEditViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation,
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id ,DepartmentEditViewModel viewModel)
        {       //if(!ModelState.IsValid) return View(viewModel);
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedDepartment = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Name = viewModel.Name,
                        Code = viewModel.Code,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateOfCreation,
                    };
                    int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is not Update");
                        //return View(viewModel);
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        //return View(viewModel);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                    }

                }
            }
            return View(viewModel);
            

        }
        #endregion

        #region 4-Delete Department
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var departement=_departmentService.GetDepartmentById(id.Value);
        //    if (departement is null) return NotFound();
        //    return View(departement);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();
            try
            {
              bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is Not Deleted");
                    return RedirectToAction(nameof(Delete),new { id });
                }

            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }

            }
        }
        #endregion


    }
}
