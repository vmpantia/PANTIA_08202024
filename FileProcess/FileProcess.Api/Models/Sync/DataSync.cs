using FileProcess.Api.Models.Enums;

namespace FileProcess.Api.Models.Sync
{
    public sealed record DataSync<TKeyProperty>(SyncAction Action, TKeyProperty Id) { }
}
