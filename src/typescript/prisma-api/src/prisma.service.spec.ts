import { PrismaClient } from "@prisma/client"
import { runTransaction } from "./run-transaction"
import { race } from "rxjs"

describe('Database access', () => {
  const prisma = new PrismaClient()
  prisma.$connect()

  test('add runner', async () => {
    await runTransaction(prisma, async (tx) => {
      // Create
      await tx.race.create({
        data: {
          name: 'New York City Marathon',
          date: new Date(),
          distanceKm: 5,
        }
      })

      // Read
      const race = await tx.race.findFirst({
        where: {
          name: 'New York City Marathon'
        }
      })

      expect(race).not.toBeNull()
      expect(race.name).toBe('New York City Marathon')
    })
  })

  test('simple relations', async () => {
    await runTransaction(prisma, async (tx) => {
      // Create
      const ada = await tx.runner.create({
        data: {
          name: 'Ada Lovelace',
          email: 'ada@example.org',
          country: 'United Kingdom',
        }
      })

      await tx.race.create({
        data: {
          name: 'New York City Marathon',
          date: new Date(),
          distanceKm: 5,
          runners: {
            create: {
              runnerId: ada.id,
              position: 1,
              bibNumber: 1,
              time: 120,
            }
          }
        }
      })

      // Read
      const loadedAda = await tx.runner.findFirst({
        where: {
          email: 'ada@example.org'
        },
        include: {
          races: true
        }
      })

      expect(loadedAda).not.toBeNull()
      expect(loadedAda.races.length).toBe(1)
    })
  })

  test('read use cases', async () => {
    await runTransaction(prisma, async (tx) => {
      // Create
      const ada = await tx.runner.create({
        data: {
          name: 'Ada Lovelace',
          email: 'ada@example.org',
          country: 'United Kingdom',
        }
      })

      const alan = await tx.runner.create({
        data: {
          name: 'Alan Turing',
          email: 'alan@example.org',
          country: 'United Kingdom',
        }
      })

      await tx.race.create({
        data: {
          name: 'New York City Marathon',
          date: new Date(),
          distanceKm: 42.195,
          runners: {
            create: {
              runnerId: ada.id,
              bibNumber: 1,
              position: 1,
              time: 120,
            }
          }
        }
      })

      await tx.race.create({
        data: {
          name: 'Boston Marathon',
          date: new Date(),
          distanceKm: 42.195,
          runners: {
            create: {
              runnerId: ada.id,
              bibNumber: 1,
              position: 5,
              time: 145,
            }
          }
        }
      })

      await tx.race.create({
        data: {
          name: 'Badwater 135',
          date: new Date(),
          distanceKm: 217,
          runners: {
            create: {
              runnerId: ada.id,
              bibNumber: 1,
              position: 15,
              time: 820,
            }
          }
        }
      })

      await tx.race.create({
        data: {
          name: 'Hardrock 100',
          date: new Date(),
          distanceKm: 160.9,
          runners: {
            create: {
              runnerId: alan.id,
              bibNumber: 1,
              position: 15,
              time: 700,
            }
          }
        }
      })

      await tx.race.create({
        data: {
          name: 'Spartathlon',
          date: new Date(),
          distanceKm: 246,
          runners: {
            create: {
              runnerId: alan.id,
              bibNumber: 1,
              position: 9,
              time: 840,
            }
          }
        }
      })

      // Read
      const loadedRunners = await tx.runner.findMany({
        include: {
          races: {
            include: {
              race: true
            }
          }
        }
      })

      expect(loadedRunners.length).toBe(2)

      const loadedAda = await tx.runner.findFirst({
        where: {
          email: 'ada@example.org'
        },
        include: {
          races: {
            include: {
              race: true
            }
          }
        }
      })

      expect(loadedAda).not.toBeNull()

      const loadedAda2 = await tx.runner.findFirst({
        where: { email: 'ada@example.org' },
        include: {
          races: {
            where: {
              AND: [
                { position: { lte: 10 } },
                { time: { lte: 120 } },
                {
                  race: {
                    name: { contains: 'New' }
                  }
                }
              ]
            }
          }
        }
      })

      expect(loadedAda2).not.toBeNull()
      expect(loadedAda2.races).not.toBeNull()
      expect(loadedAda2.races.length).toBe(1)

      const loadedRunners2 = await tx.runner.findMany({
        where: {
          races: {
            some: {
              AND: [
                { position: { lte: 10 } },
                { time: { lte: 120 } },
                {
                  race: {
                    name: { contains: 'New' }
                  }
                }
              ]
            }
          }
        },
      })

      expect(loadedRunners2).not.toBeNull()
      expect(loadedRunners2.length).toBe(1)

      const loadedAdasTop10Races = await tx.raceResult.findMany({
        select:{
          runner: { select: { name: true } },
          race: { select: { name: true } },
          position: true,
          time: true
        },
        where: {
          runner: { email: 'ada@example.org' },
          position: { lte: 10 }
        },
        orderBy: { position: 'asc' }
      })

      console.log(loadedAdasTop10Races)

      expect(loadedAdasTop10Races.length).toBe(2)
    })
  })
})
