# Stage 1: Build stage (SDK image)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# ����������� ���� ������ ������� � ���������
COPY . ./

# ���������� Blazor WebAssembly �������
RUN dotnet publish ./BlazorApp1.csproj -c Release -o /app/output

# Stage 2: Run stage (Nginx image)
FROM nginx:alpine

# ���������� ���������� ������� ������� ��� Nginx
WORKDIR /usr/share/nginx/html

# ���������� �������������� ����� �� build-env ������ � �������� ���������
COPY --from=build-env /app/output/wwwroot .

# ���������� ����������� ���� ������������ nginx (��� �������������)
COPY nginx.conf /etc/nginx.conf

# ������� ���� 80
EXPOSE 80
