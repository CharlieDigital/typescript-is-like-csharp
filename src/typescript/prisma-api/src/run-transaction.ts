/**
 * [CC] We add this to make it easier to run transactions in tests.
 */
import { Prisma, PrismaClient } from '@prisma/client'
import { DefaultArgs } from '@prisma/client/runtime/library'

type PrismaTx = Omit<PrismaClient<Prisma.PrismaClientOptions, never, DefaultArgs>, "$connect" | "$disconnect" | "$on" | "$transaction" | "$use" | "$extends">

export async function runTransaction(prisma: PrismaClient, testFn: (tx: PrismaTx) => Promise<void>) {
  // Start a transaction manually
  const tx = await prisma.$transaction(async (transactionPrisma) => {
    // Run test function with transaction Prisma client
    await testFn(transactionPrisma)

    // Throw an error at the end to force rollback
    throw new Error('ROLLBACK')
  }).catch(e => {
    if (e.message !== 'ROLLBACK') {
      throw e
    }
  })
}
