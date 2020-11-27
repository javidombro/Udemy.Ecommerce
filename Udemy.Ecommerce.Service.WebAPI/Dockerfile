#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Udemy.Ecommerce.Service.WebAPI/Udemy.Ecommerce.Service.WebAPI.csproj", "Udemy.Ecommerce.Service.WebAPI/"]
COPY ["Udemy.Ecommerce.Application.Main/Udemy.Ecommerce.Application.Main.csproj", "Udemy.Ecommerce.Application.Main/"]
COPY ["Udemy.Ecommerce.Domain.Interface/Udemy.Ecommerce.Domain.Interface.csproj", "Udemy.Ecommerce.Domain.Interface/"]
COPY ["Udemy.Ecommerce.Domain.Entity/Udemy.Ecommerce.Domain.Entity.csproj", "Udemy.Ecommerce.Domain.Entity/"]
COPY ["Udemy.Ecommerce.Application.Interface/Udemy.Ecommerce.Application.Interface.csproj", "Udemy.Ecommerce.Application.Interface/"]
COPY ["Udemy.Ecommerce.Transversal.Common/Udemy.Ecommerce.Transversal.Common.csproj", "Udemy.Ecommerce.Transversal.Common/"]
COPY ["Udemy.Ecommerce.Application.DTO/Udemy.Ecommerce.Application.DTO.csproj", "Udemy.Ecommerce.Application.DTO/"]
COPY ["Udemy.Ecommerce.Domain.Core/Udemy.Ecommerce.Domain.Core.csproj", "Udemy.Ecommerce.Domain.Core/"]
COPY ["Udemy.Ecommerce.Infraestructure.Interface/Udemy.Ecommerce.Infraestructure.Interface.csproj", "Udemy.Ecommerce.Infraestructure.Interface/"]
COPY ["Udemy.Ecommerce.Infraestructure.Repository/Udemy.Ecommerce.Infraestructure.Repository.csproj", "Udemy.Ecommerce.Infraestructure.Repository/"]
COPY ["Udemy.Ecommerce.Transversal.Mapper/Udemy.Ecommerce.Transversal.Mapper.csproj", "Udemy.Ecommerce.Transversal.Mapper/"]
COPY ["Udemy.Ecommerce.Infraestructure.Data/Udemy.Ecommerce.Infraestructure.Data.csproj", "Udemy.Ecommerce.Infraestructure.Data/"]
RUN dotnet restore "Udemy.Ecommerce.Service.WebAPI/Udemy.Ecommerce.Service.WebAPI.csproj"
COPY . .
WORKDIR "/src/Udemy.Ecommerce.Service.WebAPI"
RUN dotnet build "Udemy.Ecommerce.Service.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Udemy.Ecommerce.Service.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Udemy.Ecommerce.Service.WebAPI.dll"]