using Microsoft.AspNetCore.Identity;
using MongoDB.EntityFrameworkCore;

namespace api_flash_deck.Models;

[Collection("Roles")]
public class AppRoleIdentity : IdentityRole<Guid>
{ }