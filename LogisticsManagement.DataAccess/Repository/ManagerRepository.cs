using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly LogisticsManagementContext _db;
        public ManagerRepository(LogisticsManagementContext db)
        {
            _db = db;
        }


        #region Manage Inventory Category
        public async Task<List<InventoryCategory>> GetInventoryCategories()
        {
            try
            {
                return await _db.InventoryCategories.Where(c => c.IsActive == true).ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<InventoryCategory?> GetInventoryCategory(int id)
        {
            try
            {
                return await _db.InventoryCategories.Where(c => c.IsActive == true).FirstOrDefaultAsync(inv => inv.Id == id);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int> AddInventoryCategory(InventoryCategory category)
        {
            try
            {
                InventoryCategory existingCategory = await _db.InventoryCategories.FirstOrDefaultAsync(c => c.Name ==  category.Name);
                if (existingCategory == null)
                {
                    var entry = _db.InventoryCategories.Add(category);
                    if(await _db.SaveChangesAsync() > 0)
                    {
                        Console.WriteLine("id : "+ entry.Entity.Id);
                        return entry.Entity.Id;
                    }
                    return -1;
                }
                else
                {
                    if(existingCategory.IsActive == true)
                    {
                        return 0;
                    }
                    else
                    {
                        existingCategory.IsActive = true;
                        _db.InventoryCategories.Update(existingCategory);
                        if(await _db.SaveChangesAsync() > 0)
                        {
                            return existingCategory.Id;
                        }
                        return -1;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> RemoveInventoryCategory(int id)
        {
            try
            {
                var category = await _db.InventoryCategories.FindAsync(id);
                if (category != null)
                {
                    category.IsActive = false;
                    _db.InventoryCategories.Update(category);
                    return await _db.SaveChangesAsync();
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        #endregion



        #region Manage Inventory
        public async Task<List<Inventory>> GetInventories()
        {
            try
            {
                return await _db.Inventories.Where(inv => inv.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            };
        }

        public async Task<Inventory?> GetInventory(int id)
        {
            try
            {
                return await _db.Inventories.Where(inv => inv.IsActive == true).FirstOrDefaultAsync(inv => inv.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int> AddInventory(Inventory inventory)
        {
            try
            {
                Inventory existingInventory = await _db.Inventories.FirstOrDefaultAsync(inv => inv.Name == inventory.Name);
                if (existingInventory == null)
                {
                    var inv = _db.Inventories.Add(inventory);

                    if(await _db.SaveChangesAsync() > 0)
                    {
                        return inv.Entity.Id;
                    }
                    return -1;
                }
                else
                {
                    if (existingInventory.IsActive == true)
                    {
                        return 0;
                    }
                    else
                    {
                        existingInventory.IsActive = true;
                        _db.Inventories.Update(inventory);
                        if(await _db.SaveChangesAsync() > 0)
                        {
                            return existingInventory.Id;
                        }
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> RemoveInventory(int id)
        {
            try
            {
                var inventory = await _db.Inventories.FindAsync(id);
                if (inventory != null)
                {
                    inventory.IsActive = false;
                    _db.Inventories.Update(inventory);
                    return await _db.SaveChangesAsync();
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> UpdateInvetory(Inventory inventory)
        {
            try
            {
                var existingInventory = _db.Inventories.Local.FirstOrDefault(inv => inv.Id == inventory.Id);
                if (existingInventory != null)
                {
                    _db.Entry(existingInventory).State = EntityState.Detached;
                }

                _db.Entry(inventory).State = EntityState.Modified;
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> PutInventory(Inventory inventory)
        {
            try
            {
                if (!_db.Inventories.Local.Any(inv => inv.Id == inventory.Id))
                {
                    _db.Inventories.Attach(inventory);
                }
                _db.Entry(inventory).State = EntityState.Modified;
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }



        #endregion



        #region Manage Vehicle Type
        public async Task<List<VehicleType>> GetVehicleType()
        {
            try
            {
                return await _db.VehicleTypes.Where(v => v.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion



        #region Manage Vehicles
        public async Task<List<Vehicle>> GetVehicles()
        {
            try
            {
                return await _db.Vehicles.Include(v => v.VehicleType).Where(v => v.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            };
        }
        public async Task<int> AddVehicle(Vehicle vehicle)
        {
            try
            {
                Vehicle? existingVehicle = await _db.Vehicles.FirstOrDefaultAsync(v => v.VehicleNumber == vehicle.VehicleNumber);
                if (existingVehicle == null)
                {
                    _db.Vehicles.Add(vehicle);
                    return await _db.SaveChangesAsync();
                }
                else
                {
                    if (existingVehicle.IsActive == true)
                    {
                        return 0;
                    }
                    else
                    {
                        existingVehicle.IsActive = true;
                        _db.Vehicles.Update(vehicle);
                        return await _db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        public async Task<int> RemoveVehicle(int id)
        {
            try
            {
                var vehicle = await _db.Vehicles.FindAsync(id);
                if (vehicle != null)
                {
                    vehicle.IsActive = false;
                    _db.Vehicles.Update(vehicle);
                    return await _db.SaveChangesAsync();
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        #endregion


        #region Manage Users
        public async Task<List<User>> GetUsers()
        {
            try
            {
                return await _db.Users
                    .Include(u => u.UserDetails)
                    .Where(u => u.IsActive == true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            };
        }
        #endregion




        #region Manage Orders
        public async Task<List<Order>> GetOrders()
        {
            try
            {
                return await _db.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.OrderStatus) .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            };
        }
        #endregion

    }
}
