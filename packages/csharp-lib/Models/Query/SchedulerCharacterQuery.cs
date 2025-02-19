﻿using Wowthing.Lib.Enums;

namespace Wowthing.Lib.Models.Query;

[Keyless]
public class SchedulerCharacterQuery
{
    public long UserId { get; set; }
    public int? AccountId { get; set; }
    public WowRegion Region { get; set; }
    public string RealmSlug { get; set; }
    public int CharacterId { get; set; }
    public string CharacterName { get; set; }
    public DateTime LastApiCheck { get; set; }
    public DateTime LastApiModified { get; set; }
        
    public static string SqlQuery = @"
SELECT  c.id AS character_id,
        c.account_id AS account_id,
        c.name AS character_name,
        r.region,
        r.slug AS realm_slug,
        a.user_id,
        c.last_api_check,
        c.last_api_modified
FROM    player_character c
INNER JOIN player_account a ON c.account_id = a.id
INNER JOIN wow_realm r ON c.realm_id = r.id
WHERE (
    c.account_id IS NOT NULL AND
    (current_timestamp - c.last_api_check) > (
        '8 hours'::interval +
        ('1 hour'::interval * c.delay_hours)
    )
)
ORDER BY c.delay_hours, c.last_api_check
LIMIT 500
";
}