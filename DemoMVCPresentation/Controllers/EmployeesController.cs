using DemoBusinessLogic.Data_Transfer_Objects;
using DemoBusinessLogic.Data_Transfer_Objects.EmployeeDtos;
using DemoBusinessLogic.Services.Classes;
using DemoBusinessLogic.Services.Interfaces;
using DemoDataAccess.Models.EmployeeModel;
using DemoDataAccess.Models.Shared.Enums;
using DemoMVCPresentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVCPresentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService
        , ILogger<EmployeesController> logger , IWebHostEnvironment environment 
        , IDepartmentService departmentService) : Controller
         
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployee(EmployeeSearchName);
            return View(Employees);
        }

        #region Create Employee

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel )
        {
            if (ModelState.IsValid) //Server Side Validation.
            {
                try
                {
                    //Mapping:
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Email = employeeViewModel.Email,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender = employeeViewModel.Gender,  
                        HiringDate = employeeViewModel.HiringDate,  
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        IsActive = employeeViewModel.IsActive,  
                        Salary= employeeViewModel.Salary,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image,

                    };

                    int Result = _employeeService.CreateEmployee(employeeDto);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't Be Created");
                        return View(employeeDto);
                    }
                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        logger.LogError(ex.Message);
                    }

                }
            }
            return View(employeeViewModel);
        }

        #endregion

        #region Details Of Employee
        public IActionResult Details(int? id)
        {   
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeebyId(id.Value);
            return employee is null ? NotFound() : View(employee);  
        }

        #endregion

        #region Edit Employee
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeebyId(id.Value);
            if(employee is null) return NotFound();
            var employeeViewModel = new EmployeeViewModel()
            {
               
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId=employee.DepartmentId
            };
            return View(employeeViewModel);
        }


        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if(!id.HasValue ) return BadRequest();
            if(!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeViewModel.Name,
                    Age = employeeViewModel.Age,
                    Address = employeeViewModel.Address,
                    Email = employeeViewModel.Email,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Gender = employeeViewModel.Gender,
                    HiringDate = employeeViewModel.HiringDate,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    IsActive = employeeViewModel.IsActive,
                    Salary = employeeViewModel.Salary,
                    DepartmentId = employeeViewModel.DepartmentId,

                };
                var Result = _employeeService.UpdateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Updated");
                    return View(employeeViewModel);
                }

            }
            catch (Exception ex) 
            {
                if(environment.IsDevelopment())
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeViewModel);
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView",ex);
                }
            
            
            }
        }


        #endregion

        #region Delete Employee
        [HttpPost]
        public IActionResult Delete(int id) 
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is Not Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }

            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }

            }


        }

        #endregion
    }

}
