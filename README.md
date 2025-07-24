# MyApi - .NET Core Web API

This is a sample .NET Core Web API hosted on Heroku using Docker.

## 🚀 How to Deploy on Heroku

1. Build API
2. Create `Dockerfile` (included)
3. Login to Heroku and Docker
4. Deploy:

```bash
heroku login
heroku container:login
heroku create myapi-heroku
heroku container:push web -a myapi-heroku
heroku container:release web -a myapi-heroku
heroku open -a myapi-heroku
