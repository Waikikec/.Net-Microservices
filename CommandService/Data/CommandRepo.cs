using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(int platformId, Command command)
        {
            if (command != null)
            {
                command.PlatformId = platformId;
                _context.Commands.Add(command);
            }
            else
            {
                throw new ArgumentNullException(nameof(command));
            }
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform != null)
            {
                _context.Platforms.Add(platform);
            }
            else
            {
                throw new ArgumentNullException(nameof(platform));
            }
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId && c.Id == commandId)
                .FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands
                .Where(p => p.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name);
        }

        public bool IsPlatformExists(int platformId)
        {
            return _context.Platforms
                .Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}