import {
  Column,
  CreateDateColumn,
  Entity,
  OneToMany,
  PrimaryGeneratedColumn,
} from 'typeorm';
import { OrderItem } from './orderItem.entity';

export enum OrderStatus {
  PENDING = 'pending',
  PAID = 'paid',
  FAILED = 'failed',
}

export type CreateOrderCommand = {
  client_id: number;
  items: {
    product_id: string;
    quantity: number;
    price: number;
  }[];
};

@Entity()
export class Order {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column({ type: 'decimal', precision: 10, scale: 2 })
  total: number;

  @Column()
  client_id: number; //usuario autenticado

  @Column()
  status: OrderStatus = OrderStatus.PENDING;

  @CreateDateColumn()
  created_at: Date;

  @OneToMany(() => OrderItem, (item) => item.order, {
    cascade: ['insert'],
    eager: true,
  })
  items: OrderItem[];

  static create(input: CreateOrderCommand) {
    const order = new Order();
    order.client_id = input.client_id;
    order.items = input.items.map((item) => OrderItem.create(item));

    order.total = order.items.reduce(
      (acc, item) => acc + item.price * item.quantity,
      0,
    );

    return order;
  }
}
