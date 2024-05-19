# poidotoho-game
## Running the Game Locally with docker build
To run the game locally, execute the following commands:
```sh
git clone https://github.com/FEIProjectTeam/poidotoho-game.git
cd poidotoho-game
git clone --recurse-submodules https://github.com/FEIProjectTeam/poidotoho-server.git
docker-compose up
```
> **_NOTE:_**  For Windows users, Docker Desktop must be installed.

## git fetch
### Run this command for better updates
`git config --global alias.pullall '!git pull && git submodule update --init --recursive'`

### With SSH
```sh
git clone git@github.com:FEIProjectTeam/poidotoho-game.git 
cd poidotoho-game
git clone --recurse-submodules git@github.com:FEIProjectTeam/poidotoho-server.git
```

### With HTTPS
```sh
git clone https://github.com/FEIProjectTeam/poidotoho-game.git
cd poidotoho-game
git clone --recurse-submodules https://github.com/FEIProjectTeam/poidotoho-server.git
```

## Docker build commands
```sh
docker build -t feipt/poidotoho-game:latest .
```

```sh
docker run -p 8080:8443 feipt/poidotoho-game
```

```sh
docker push feipt/poidotoho-game:latest  
```