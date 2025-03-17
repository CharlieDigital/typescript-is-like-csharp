-- CreateTable
CREATE TABLE "runner" (
    "id" SERIAL NOT NULL,
    "name" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "country" TEXT NOT NULL,

    CONSTRAINT "runner_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "race" (
    "id" SERIAL NOT NULL,
    "name" TEXT NOT NULL,
    "date" TIMESTAMP(3) NOT NULL,
    "distanceKm" DOUBLE PRECISION NOT NULL,

    CONSTRAINT "race_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "race_result" (
    "runnerId" INTEGER NOT NULL,
    "raceId" INTEGER NOT NULL,
    "position" INTEGER NOT NULL,
    "time" INTEGER NOT NULL,
    "bibNumber" INTEGER NOT NULL,

    CONSTRAINT "race_result_pkey" PRIMARY KEY ("runnerId","raceId")
);

-- CreateIndex
CREATE UNIQUE INDEX "runner_email_key" ON "runner"("email");

-- CreateIndex
CREATE INDEX "runner_email_idx" ON "runner"("email");

-- CreateIndex
CREATE INDEX "race_date_idx" ON "race"("date");

-- CreateIndex
CREATE INDEX "race_result_bibNumber_idx" ON "race_result"("bibNumber");

-- AddForeignKey
ALTER TABLE "race_result" ADD CONSTRAINT "race_result_runnerId_fkey" FOREIGN KEY ("runnerId") REFERENCES "runner"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "race_result" ADD CONSTRAINT "race_result_raceId_fkey" FOREIGN KEY ("raceId") REFERENCES "race"("id") ON DELETE RESTRICT ON UPDATE CASCADE;
