FROM microsoft/aspnetcore

RUN apt-get update && apt-get install -y gnupg apt-transport-https

RUN curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg && \
    mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg && \
    sh -c 'echo "deb https://packages.microsoft.com/repos/microsoft-debian-stretch-prod stretch main" > /etc/apt/sources.list.d/dotnetdev.list' && \
    apt-get update && apt-get install -y aspnetcore-runtime-2.1

# update timezone
ENV TZ 'Asia/Ho_Chi_Minh'
RUN echo $TZ > /etc/timezone && \
    apt-get update && apt-get install -y tzdata && \
    rm /etc/localtime && \
    ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata && \
    apt-get clean

COPY ./app /app

WORKDIR /app

EXPOSE 80

ENTRYPOINT [ "dotnet", "Giveaway.API.dll" ]
