import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { getDataSourceToken } from '@nestjs/typeorm';
import { DataSource } from 'typeorm';
import { v4 as uuidv4 } from 'uuid';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  await app.init();

  const dataSource = app.get<DataSource>(getDataSourceToken());

  await dataSource.synchronize(true);

  const productRepo = dataSource.getRepository('Product');

  await productRepo.insert([
    {
      id: uuidv4(),
      name: 'Product 1',
      description: 'Description 1',
      price: 100,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 2',
      description: 'Description 2',
      price: 200,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 3',
      description: 'Description 3',
      price: 300,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 4',
      description: 'Description 4',
      price: 400,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 5',
      description: 'Description 5',
      price: 500,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 6',
      description: 'Description 6',
      price: 600,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 7',
      description: 'Description 7',
      price: 700,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 8',
      description: 'Description 8',
      price: 800,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 9',
      description: 'Description 9',
      price: 900,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 10',
      description: 'Description 10',
      price: 1000,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 11',
      description: 'Description 11',
      price: 1100,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 12',
      description: 'Description 12',
      price: 1200,
      image_url: 'https://via.placeholder.com/150',
    },
    {
      id: uuidv4(),
      name: 'Product 13',
      description: 'Description 13',
      price: 1300,
      image_url: 'https://via.placeholder.com/150',
    },
  ]);
  app.close();
}
bootstrap();
