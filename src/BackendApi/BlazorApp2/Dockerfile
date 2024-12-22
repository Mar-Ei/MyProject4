# Stage 1: Build stage (SDK image)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Копирование всех файлов проекта в контейнер
COPY . ./

# Публикация Blazor WebAssembly проекта
RUN dotnet publish ./BlazorApp1.csproj -c Release -o /app/output

# Stage 2: Run stage (Nginx image)
FROM nginx:alpine

# Установить правильный рабочий каталог для Nginx
WORKDIR /usr/share/nginx/html

# Копировать опубликованные файлы из build-env стадии в конечный контейнер
COPY --from=build-env /app/output/wwwroot .

# Копировать собственный файл конфигурации nginx (при необходимости)
COPY nginx.conf /etc/nginx.conf

# Открыть порт 80
EXPOSE 80
