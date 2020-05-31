using AutoMapper;
using JagraTaskManager.Server.Models;
using JagraTaskManager.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<OrganizationUser, UserForListDto>()
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(ou => ou.User.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(ou => ou.User.LastName)
                )
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(ou => ou.User.Id)
                )
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(ou => ou.User.UserName)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(ou => ou.User.Email)
                )
                .ForMember(
                    dest => dest.Created,
                    opt => opt.MapFrom(ou => ou.User.Created)
                );
            CreateMap<TeamUser, UserForListDto>()
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(tu => tu.User.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(tu => tu.User.LastName)
                )
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(tu => tu.User.Id)
                )
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(tu => tu.User.UserName)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(tu => tu.User.Email)
                )
                .ForMember(
                    dest => dest.Created,
                    opt => opt.MapFrom(tu => tu.User.Created)
                );
            CreateMap<Invitation, InvitationForListDto>()
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(i => i.User.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(i => i.User.LastName)
                )
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(i => i.User.Id)
                )
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(i => i.User.UserName)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(i => i.User.Email)
                )
                .ForMember(
                    dest => dest.OrganizationId,
                    opt => opt.MapFrom(i => i.OrganizationId)
                )
                .ForMember(
                    dest => dest.OrganizationName,
                    opt => opt.MapFrom(i => i.Organization.Name)
                );
            CreateMap<Organization, OrganizationForListDto>();
            CreateMap<Organization, OrganizationForInvitationDto>();
            CreateMap<TicketTag, TicketTagForListDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(tt => tt.Tag.Id))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(tt => tt.Tag.Name))
                .ForMember(
                    dest => dest.Color,
                    opt => opt.MapFrom(tt => tt.Tag.Color));
            CreateMap<TicketStatus, TicketStatusForListDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(ts => ts.Status.Id))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(ts => ts.Status.Name));
            CreateMap<TicketWatch, UserForListDto>()
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(tw => tw.User.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(tw => tw.User.LastName)
                )
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(tw => tw.User.Id)
                )
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(tw => tw.User.UserName)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(tw => tw.User.Email)
                )
                .ForMember(
                    dest => dest.Created,
                    opt => opt.MapFrom(tw => tw.User.Created)
                );
            CreateMap<Ticket, TicketForListDto>();
            CreateMap<Team, TeamForListDto>();
        }
    }
}
