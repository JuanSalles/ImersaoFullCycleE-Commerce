import { Injectable } from '@nestjs/common';
import { CreateOrderDto } from './dto/create-order.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Order } from './entities/order.entity';
import { Product } from '../products/entities/product.entity';
import { OrderItem } from './entities/orderItem.entity';

@Injectable()
export class OrdersService {
  constructor(
    @InjectRepository(Order) private orderRepo: Repository<Order>,
    @InjectRepository(OrderItem) private orderItemRepo: Repository<OrderItem>,
    @InjectRepository(Product) private productRepo: Repository<Product>,
  ) {}

  async create(createOrderDto: CreateOrderDto) {
    const itemsPromises = createOrderDto.items.map(async (item) => ({
      product_id: item.product_id,
      price: await this.productRepo
        .findOne({
          where: { id: item.product_id },
        })
        .then((product) => product.price),
      quantity: item.quantity,
    }));

    Promise.all(itemsPromises).then((items) => {
      const order = Order.create({
        client_id: createOrderDto.client_id,
        items: items,
      });
      this.orderRepo.save(order);
    });
  }

  findAll() {
    return this.orderRepo.find();
  }

  findOne(id: number) {
    return `This action returns a #${id} order`;
  }

  // findItemsByOrderId(id: string) {
  //   return this.orderRepo.findBy({ where: { id }, relations: ['items'] });
  // }
}
