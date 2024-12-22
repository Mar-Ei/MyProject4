FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish ./BlazorApp1.csproj -c Release -o output

FROM nginx:alpine
WORKDIR /user/share/nginx/html
COPY --from=build-env /app/output/wwwrot .

COPY nginx.conf /etc/nginx.conf

EXPOSE 80
