# FinanceTracker
Experimental app to (you guessed it) track finances

## Running locally

Install docker if you haven't already. Spin the app up locally by running:
```bash
docker-compose up -d web
```

Navigate to [http://localhost:3000/](http://localhost:3000/) in your browser and upload your expenses!

## Limitations
- Currently only supports csv downloads from Chase.
- Headers must include: Transaction Date, Post Date, Description, Category, Type, and Amoount.
- You can use [these test expenses](https://github.com/janice-wong/FinanceTracker/blob/master/TestExpenses.csv) as a sample file to upload.
