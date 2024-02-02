import { Product } from '../../products/entities/product.entity';
import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';
import { Order } from './order.entity';

export type CreateOrderItemCommand = {
  product_id: string;
  quantity: number;
  price: number;
};

@Entity()
export class OrderItem {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({ type: 'int' })
  quantity: number;

  @Column({ type: 'decimal', precision: 10, scale: 2 })
  price: number;

  @ManyToOne(() => Product, { eager: true })
  @JoinColumn({ name: 'product_id' })
  product: Product;

  @Column()
  product_id: string;

  @ManyToOne(() => Order, (order) => order.items)
  @JoinColumn({ name: 'order_id' })
  order: Order;

  static create(data: CreateOrderItemCommand) {
    const orderItem = new OrderItem();
    orderItem.quantity = data.quantity;
    orderItem.price = data.price;
    orderItem.product_id = data.product_id;

    return orderItem;
  }
}