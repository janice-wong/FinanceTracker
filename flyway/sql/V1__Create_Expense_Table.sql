CREATE TYPE expense_category AS ENUM (
    'Undefined',
    'FoodAndDrink',
    'Groceries',
    'Shopping',
    'HealthAndWellness',
    'Travel',
    'FeesAndAdjustments',
    'Gas',
    'Entertainment',
    'Misc'
);

CREATE TYPE expense_type AS ENUM ('Undefined', 'Sale', 'Adjustment', 'Return', 'Payment');

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE public.expense (
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    transaction_date date NOT NULL,
    post_date date NOT NULL,
    description character varying(100) NOT NULL,
    amount decimal NOT NULL,
    expense_category expense_category NOT NULL,
    expense_type expense_type NOT NULL,
    import_date date NOT NULL
);
