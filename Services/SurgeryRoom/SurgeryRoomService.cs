
using TodoApi.Models.Shared;

public class SurgeryRoomService : ISurgeryRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISurgeryRoomRepository surgeryRoomRepository;

    private readonly ILogger<ISurgeryRoomService> _logger;
    private readonly IConfiguration _config;
    private SurgeryRoomMapper mapper = new SurgeryRoomMapper();

    public SurgeryRoomService(IUnitOfWork unitOfWork, ISurgeryRoomRepository surgeryRoomRepository, ILogger<ISurgeryRoomService> logger,
        IConfiguration config)
    {
        this._unitOfWork = unitOfWork;
        this.surgeryRoomRepository = surgeryRoomRepository;
        this._logger = logger;
        this._config = config;
    }

    public async Task<List<SurgeryRoomDTO>> GetSurgeryRooms()
    {
        try
        {
            List<SurgeryRoom> surgeryRooms = await surgeryRoomRepository.GetAllAsync();
            List<SurgeryRoomDTO> dtos = new List<SurgeryRoomDTO>();
            foreach (SurgeryRoom surgeryRoom in surgeryRooms)
            {
                dtos.Add(mapper.ToDto(surgeryRoom));
            }

            return dtos;
        }
        catch (Exception e)
        {
            this._logger.LogError(e, "Error getting surgeryRooms");
            throw;
        }
    }

}