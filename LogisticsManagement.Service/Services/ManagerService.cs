using AutoMapper;
using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository.IRepository;
using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;

namespace LogisticsManagement.Service.Services
{
    public class ManagerService:IManagerService
    {
        private readonly IManagerRepository _managerRepo;
        private readonly IMapper _mapper;

        public ManagerService(IManagerRepository managerRepo, IMapper mapper) 
        {
            _managerRepo = managerRepo;
            _mapper = mapper;
        }


        #region Manage Inventory Category
        public async Task<List<InventoryCategoryDTO>> GetInventoryCategories()
        {
            try
            {
                List<InventoryCategory> categories = await _managerRepo.GetInventoryCategories();
                return _mapper.Map<List<InventoryCategoryDTO>>(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<InventoryCategoryDTO>? GetInventoryCategory(int id)
        {
            try
            {
                InventoryCategory? category = await _managerRepo.GetInventoryCategory(id);
                return _mapper.Map<InventoryCategoryDTO>(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int> AddInventoryCategory(InventoryCategoryDTO category)
        {
            try
            {
                InventoryCategory inventoryCategory = _mapper.Map<InventoryCategory>(category);
                return await _managerRepo.AddInventoryCategory(inventoryCategory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }


        public async Task<int> RemoveInventoryCategory(int id)
        {
            try
            {
                return await _managerRepo.RemoveInventoryCategory(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        #endregion



        #region Manage Inventory
        public async Task<List<InventoryDTO>> GetInventories()
        {
            try
            {
                List<Inventory> inventories = await _managerRepo.GetInventories();
                return _mapper.Map<List<InventoryDTO>>(inventories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<InventoryDTO> GetInventory(int id)
        {
            try
            {
                Inventory? inventory = await _managerRepo.GetInventory(id);
                return _mapper.Map<InventoryDTO>(inventory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int> AddInventory(InventoryDTO inventory)
        {
            try
            {
                Inventory inv = _mapper.Map<Inventory>(inventory);
                return await _managerRepo.AddInventory(inv);
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
                return await _managerRepo.RemoveInventory(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> UpdateInventory(InventoryDTO inventory)
        {
            try
            {
                Inventory inv = _mapper.Map<Inventory>(inventory);
                return await _managerRepo.UpdateInvetory(inv);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> PutInventory(InventoryDTO inventory)
        {
            try
            {
                Inventory inv = _mapper.Map<Inventory>(inventory);
                return await _managerRepo.PutInventory(inv);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        #endregion



        #region Manage Vehicles Type
        public async Task<List<VehicleTypeDTO>> GetVehicleType()
        {
            try
            {
                List<VehicleType> vehicleTypes = await _managerRepo.GetVehicleType();
                return _mapper.Map<List<VehicleTypeDTO>>(vehicleTypes);
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion



        #region Manage Vehicles
        public async Task<List<VehicleDTO>> GetVehicles()
        {
            try
            {
                List<Vehicle> vehicles = await _managerRepo.GetVehicles();
                return _mapper.Map<List<VehicleDTO>>(vehicles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<int> AddVehicle(VehicleDTO vehicle)
        {
            try
            {
                Vehicle v = _mapper.Map<Vehicle>(vehicle);
                return await _managerRepo.AddVehicle(v);
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
                return await _managerRepo.RemoveVehicle(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        #endregion

        public async Task<List<UserWithDetailDTO>> GetUsers()
        {
            try
            {
                List<User> drivers = await _managerRepo.GetUsers();
                return _mapper.Map<List<UserWithDetailDTO>>(drivers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<List<OrderDTO>> GetOrders()
        {
            try{
                List<Order> orders = await _managerRepo.GetOrders();
                return _mapper.Map<List<OrderDTO>>(orders);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
