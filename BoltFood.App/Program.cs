using BoltFood.Service.Services.Implementations;
using BoltFood.Service.Services.Interfaces;
using System.ComponentModel.Design;

MenuService menuService = new MenuService();
await menuService.ShowMenuAsync();
IMenuService menuService2= new MenuService();
menuService2.ShowMenuAsync();





