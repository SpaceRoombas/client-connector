using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class PlayerFirmwareChange
    {

        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("player_id")]
        public string PlayerId { get; set; }
        [JsonPropertyName("robot_id")]
        public string RobotId { get; set; }
    }
}
