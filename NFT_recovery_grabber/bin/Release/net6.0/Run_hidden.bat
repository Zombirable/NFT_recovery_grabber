@echo Nft recovery grabber has been started
@echo off
taskkill /IM chromedriver.exe /f
cd /d %~dp0
NFT_recovery_grabber.exe --hidden
taskkill /IM chromedriver.exe /f