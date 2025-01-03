
using TodoApi.Models.Shared;

public class RoomTypeService : IRoomTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoomTypeRepository roomTypeRepository;

    private readonly ILogger<IRoomTypeService> _logger;
    private readonly IConfiguration _config;
    private RoomTypeMapper mapper = new RoomTypeMapper();

    public RoomTypeService(IUnitOfWork unitOfWork, IRoomTypeRepository roomTypeRepository, ILogger<IRoomTypeService> logger,
        IConfiguration config)
    {
        this._unitOfWork = unitOfWork;
        this.roomTypeRepository = roomTypeRepository;
        this._logger = logger;
        this._config = config;
    }

    public async Task<RoomTypeDTO> CreateRoomType(CreateRoomTypeDTO dto)
{
    try
    {
        
        bool roomTypeExists = await roomTypeRepository.ExistsAsync(new RoomDesignation(dto.roomDesignation));
        if (roomTypeExists)
        {
            throw new Exception("The specified room Type already exist.");
        }

       RoomType mapped = mapper.toEntity(dto);
            
            await this.roomTypeRepository.AddAsync(mapped);
            
            RoomTypeDTO mappedDto = mapper.ToDto(mapped);
            
            await this._unitOfWork.CommitAsync();
            
            return mappedDto;
        }
        catch (Exception e)
        {
        // Captura a exceção interna, se houver
            var innerExceptionMessage = e.InnerException != null ? e.InnerException.Message : "No inner exception";

             _logger.LogError(e, $"Error saving entity changes. Inner Exception: {innerExceptionMessage}");
             throw new Exception($"Error saving entity changes. Details: {e.Message} | Inner Exception: {innerExceptionMessage}", e);

            }

}


    public async Task<List<RoomTypeDTO>> GetRoomTypes()
    {
        try
        {
            List<RoomType> roomTypes = await roomTypeRepository.GetAllAsync();
            List<RoomTypeDTO> dtos = new List<RoomTypeDTO>();
            foreach (RoomType roomType in roomTypes)
            {
                dtos.Add(mapper.ToDto(roomType));
            }

            return dtos;
        }
        catch (Exception e)
        {
            this._logger.LogError(e, "Error getting roomTypes");
            throw;
        }
    }

}