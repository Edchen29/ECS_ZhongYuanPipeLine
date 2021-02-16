/*
Navicat MySQL Data Transfer

Source Server         : huaheng
Source Server Version : 50725
Source Host           : 172.16.29.45:3306
Source Database       : huahengwcs2

Target Server Type    : MYSQL
Target Server Version : 50725
File Encoding         : 65001

Date: 2019-11-13 20:19:38
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for wcsconfig
-- ----------------------------
DROP TABLE IF EXISTS `wcsconfig`;
CREATE TABLE `wcsconfig` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `warehouseCode` varchar(20) COLLATE utf8_bin DEFAULT NULL COMMENT '仓库编码',
  `code` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `name` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `value` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `remark` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsconfig
-- ----------------------------
INSERT INTO `wcsconfig` VALUES ('43', 'XT0001', 'RowCount', '行总数', '2', '用于堆垛机图像绘制', '2019-10-15 14:30:25', 'system', '2019-10-15 14:30:44', 'system');
INSERT INTO `wcsconfig` VALUES ('44', 'XT0001', 'ColumnCount', '列总数', '30', '用于堆垛机图像绘制', '2019-10-15 14:33:01', 'system', '2019-10-15 14:33:12', 'system');
INSERT INTO `wcsconfig` VALUES ('45', null, 'turnStocker', '转轨堆垛机', '1', null, '2019-11-11 09:18:59', 'admin', null, null);

-- ----------------------------
-- Table structure for wcscontainer
-- ----------------------------
DROP TABLE IF EXISTS `wcscontainer`;
CREATE TABLE `wcscontainer` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `code` varchar(50) NOT NULL DEFAULT '' COMMENT '容器编码',
  `type` varchar(50) NOT NULL DEFAULT '' COMMENT '容器类型',
  `status` varchar(50) NOT NULL DEFAULT '' COMMENT '状态',
  `printCount` int(11) NOT NULL DEFAULT '0' COMMENT '打印次数',
  `warehouseCode` varchar(50) NOT NULL DEFAULT '' COMMENT '仓库编码',
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) DEFAULT '' COMMENT '创建人',
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) DEFAULT '' COMMENT '最后更新人',
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1190 DEFAULT CHARSET=utf8mb4 COMMENT='容器表';

-- ----------------------------
-- Records of wcscontainer
-- ----------------------------
INSERT INTO `wcscontainer` VALUES ('437', 'M00001', 'M', 'empty', '2', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('438', 'M00002', 'M', 'some', '4', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('439', 'M00003', 'M', 'some', '1', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('440', 'M00004', 'M', 'empty', '1', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('441', 'M00005', 'M', 'empty', '1', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('442', 'M00006', 'M', 'empty', '1', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('443', 'M00007', 'M', 'empty', '2', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('444', 'M00008', 'M', 'empty', '1', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('445', 'M00009', 'M', 'empty', '1', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('446', 'M00010', 'M', 'empty', '1', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('447', 'M00011', 'M', 'empty', '7', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('448', 'M00012', 'M', 'empty', '1', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('449', 'M00013', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('450', 'M00014', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('451', 'M00015', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('452', 'M00016', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('453', 'M00017', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('454', 'M00018', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('455', 'M00019', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('456', 'M00020', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('457', 'M00021', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('458', 'M00022', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('459', 'M00023', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('460', 'M00024', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('461', 'M00025', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('462', 'M00026', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('463', 'M00027', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('464', 'M00028', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('465', 'M00029', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('466', 'M00030', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('467', 'M00031', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('468', 'M00032', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('469', 'M00033', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('470', 'M00034', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('471', 'M00035', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('472', 'M00036', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('473', 'M00037', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('474', 'M00038', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('475', 'M00039', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('476', 'M00040', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('477', 'M00041', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('478', 'M00042', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('479', 'M00043', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('480', 'M00044', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('481', 'M00045', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('482', 'M00046', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('483', 'M00047', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('484', 'M00048', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('485', 'M00049', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('486', 'M00050', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('487', 'M00051', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('488', 'M00052', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('489', 'M00053', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('490', 'M00054', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('491', 'M00055', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('492', 'M00056', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('493', 'M00057', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('494', 'M00058', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('495', 'M00059', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('496', 'M00060', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('497', 'M00061', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('498', 'M00062', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('499', 'M00063', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('500', 'M00064', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('501', 'M00065', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('502', 'M00066', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('503', 'M00067', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('504', 'M00068', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('505', 'M00069', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('506', 'M00070', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('507', 'M00071', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('508', 'M00072', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('509', 'M00073', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('510', 'M00074', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('511', 'M00075', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('512', 'M00076', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('513', 'M00077', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('514', 'M00078', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('515', 'M00079', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('516', 'M00080', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('517', 'M00081', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('518', 'M00082', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('519', 'M00083', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('520', 'M00084', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('521', 'M00085', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('522', 'M00086', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('523', 'M00087', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('524', 'M00088', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('525', 'M00089', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('526', 'M00090', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('527', 'M00091', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('528', 'M00092', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('529', 'M00093', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('530', 'M00094', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('531', 'M00095', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('532', 'M00096', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('533', 'M00097', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('534', 'M00098', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('535', 'M00099', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('536', 'M00100', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('537', 'M00101', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('538', 'M00102', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('539', 'M00103', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('540', 'M00104', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('541', 'M00105', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('542', 'M00106', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('543', 'M00107', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('544', 'M00108', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('545', 'M00109', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('546', 'M00110', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('547', 'M00111', 'M', 'some', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('548', 'M00112', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('549', 'M00113', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('550', 'M00114', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('551', 'M00115', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('552', 'M00116', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('553', 'M00117', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('554', 'M00118', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('555', 'M00119', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('556', 'M00120', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('557', 'M00121', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('558', 'M00122', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('559', 'M00123', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('560', 'M00124', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('561', 'M00125', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('562', 'M00126', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('563', 'M00127', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('564', 'M00128', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('565', 'M00129', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('566', 'M00130', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('567', 'M00131', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('568', 'M00132', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('569', 'M00133', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('570', 'M00134', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('571', 'M00135', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('572', 'M00136', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('573', 'M00137', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('574', 'M00138', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('575', 'M00139', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('576', 'M00140', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('577', 'M00141', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('578', 'M00142', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('579', 'M00143', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('580', 'M00144', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('581', 'M00145', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('582', 'M00146', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('583', 'M00147', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('584', 'M00148', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('585', 'M00149', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('586', 'M00150', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('587', 'M00151', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('588', 'M00152', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('589', 'M00153', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('590', 'M00154', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('591', 'M00155', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('592', 'M00156', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('593', 'M00157', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('594', 'M00158', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('595', 'M00159', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('596', 'M00160', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('597', 'M00161', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('598', 'M00162', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('599', 'M00163', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('600', 'M00164', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('601', 'M00165', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('602', 'M00166', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('603', 'M00167', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('604', 'M00168', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('605', 'M00169', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('606', 'M00170', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('607', 'M00171', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('608', 'M00172', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('609', 'M00173', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('610', 'M00174', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('611', 'M00175', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('612', 'M00176', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('613', 'M00177', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('614', 'M00178', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('615', 'M00179', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('616', 'M00180', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('617', 'M00181', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('618', 'M00182', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('619', 'M00183', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('620', 'M00184', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('621', 'M00185', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('622', 'M00186', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('623', 'M00187', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('624', 'M00188', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('625', 'M00189', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('626', 'M00190', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('627', 'M00191', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('628', 'M00192', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('629', 'M00193', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('630', 'M00194', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('631', 'M00195', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('632', 'M00196', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('633', 'M00197', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('634', 'M00198', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('635', 'M00199', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('636', 'M00200', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('637', 'M00201', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('638', 'M00202', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('639', 'M00203', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('640', 'M00204', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('641', 'M00205', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('642', 'M00206', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('643', 'M00207', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('644', 'M00208', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('645', 'M00209', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('646', 'M00210', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('647', 'M00211', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('648', 'M00212', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('649', 'M00213', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('650', 'M00214', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('651', 'M00215', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('652', 'M00216', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('653', 'M00217', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('654', 'M00218', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('655', 'M00219', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('656', 'M00220', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('657', 'M00221', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('658', 'M00222', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('659', 'M00223', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('660', 'M00224', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('661', 'M00225', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('662', 'M00226', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('663', 'M00227', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('664', 'M00228', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('665', 'M00229', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('666', 'M00230', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('667', 'M00231', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('668', 'M00232', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('669', 'M00233', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('670', 'M00234', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('671', 'M00235', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('672', 'M00236', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('673', 'M00237', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('674', 'M00238', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('675', 'M00239', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('676', 'M00240', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('677', 'M00241', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('678', 'M00242', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('679', 'M00243', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('680', 'M00244', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('681', 'M00245', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('682', 'M00246', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('683', 'M00247', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('684', 'M00248', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('685', 'M00249', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('686', 'M00250', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('687', 'M00251', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('688', 'M00252', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('689', 'M00253', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('690', 'M00254', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('691', 'M00255', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('692', 'M00256', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('693', 'M00257', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('694', 'M00258', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('695', 'M00259', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('696', 'M00260', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('697', 'M00261', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('698', 'M00262', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('699', 'M00263', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('700', 'M00264', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('701', 'M00265', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('702', 'M00266', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('703', 'M00267', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('704', 'M00268', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('705', 'M00269', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('706', 'M00270', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('707', 'M00271', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('708', 'M00272', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('709', 'M00273', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('710', 'M00274', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('711', 'M00275', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('712', 'M00276', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('713', 'M00277', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('714', 'M00278', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('715', 'M00279', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('716', 'M00280', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('717', 'M00281', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('718', 'M00282', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('719', 'M00283', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('720', 'M00284', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('721', 'M00285', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('722', 'M00286', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('723', 'M00287', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('724', 'M00288', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('725', 'M00289', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('726', 'M00290', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('727', 'M00291', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('728', 'M00292', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('729', 'M00293', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('730', 'M00294', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('731', 'M00295', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('732', 'M00296', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('733', 'M00297', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('734', 'M00298', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('735', 'M00299', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('736', 'M00300', 'M', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('737', 'S00001', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('738', 'S00002', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('739', 'S00003', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('740', 'S00004', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('741', 'S00005', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('742', 'L00001', 'L', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('743', 'S00006', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('744', 'S00007', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('745', 'S00008', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('746', 'S00009', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('747', 'S00010', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('748', 'S00011', 'S', 'empty', '0', 'XT0001', null, 'admin', null, '');
INSERT INTO `wcscontainer` VALUES ('749', 'M00301', 'M', 'empty', '0', 'XT0001', null, 'yh', null, '');
INSERT INTO `wcscontainer` VALUES ('750', 'L00002', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('751', 'L00003', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('752', 'L00004', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('753', 'L00005', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('754', 'L00006', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('755', 'L00007', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('756', 'L00008', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('757', 'L00009', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('758', 'L00010', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('759', 'L00011', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('760', 'L00012', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('761', 'L00013', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('762', 'L00014', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('763', 'L00015', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('764', 'L00016', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('765', 'L00017', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('766', 'L00018', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('767', 'L00019', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('768', 'L00020', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('769', 'L00021', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('770', 'L00022', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('771', 'L00023', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('772', 'L00024', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('773', 'L00025', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('774', 'L00026', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, 'tony');
INSERT INTO `wcscontainer` VALUES ('775', 'L00027', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('776', 'L00028', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('777', 'L00029', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('778', 'L00030', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('779', 'L00031', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('780', 'L00032', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('781', 'L00033', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('782', 'L00034', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('783', 'L00035', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('784', 'L00036', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('785', 'L00037', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('786', 'L00038', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('787', 'L00039', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('788', 'L00040', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('789', 'L00041', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('790', 'L00042', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('791', 'L00043', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('792', 'L00044', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('793', 'L00045', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('794', 'L00046', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('795', 'L00047', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('796', 'L00048', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('797', 'L00049', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('798', 'L00050', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('799', 'L00051', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('800', 'L00052', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('801', 'L00053', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('802', 'L00054', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('803', 'L00055', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('804', 'L00056', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('805', 'L00057', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('806', 'L00058', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('807', 'L00059', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('808', 'L00060', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('809', 'L00061', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('810', 'L00062', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('811', 'L00063', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('812', 'L00064', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('813', 'L00065', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('814', 'L00066', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('815', 'L00067', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('816', 'L00068', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('817', 'L00069', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('818', 'L00070', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('819', 'L00071', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('820', 'L00072', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('821', 'L00073', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('822', 'L00074', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('823', 'L00075', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('824', 'L00076', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('825', 'L00077', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('826', 'L00078', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('827', 'L00079', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('828', 'L00080', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('829', 'L00081', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('830', 'L00082', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('831', 'L00083', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('832', 'L00084', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('833', 'L00085', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('834', 'L00086', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('835', 'L00087', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('836', 'L00088', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('837', 'L00089', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('838', 'L00090', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('839', 'L00091', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('840', 'L00092', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('841', 'L00093', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('842', 'L00094', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('843', 'L00095', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('844', 'L00096', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('845', 'L00097', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('846', 'L00098', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('847', 'L00099', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('848', 'L00100', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('849', 'L00101', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('850', 'L00102', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('851', 'L00103', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('852', 'L00104', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('853', 'L00105', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('854', 'L00106', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('855', 'L00107', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('856', 'L00108', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('857', 'L00109', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('858', 'L00110', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('859', 'L00111', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('860', 'L00112', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('861', 'L00113', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('862', 'L00114', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('863', 'L00115', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('864', 'L00116', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('865', 'L00117', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('866', 'L00118', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('867', 'L00119', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('868', 'L00120', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('869', 'L00121', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('870', 'L00122', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('871', 'L00123', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('872', 'L00124', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('873', 'L00125', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('874', 'L00126', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('875', 'L00127', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('876', 'L00128', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('877', 'L00129', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('878', 'L00130', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('879', 'L00131', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('880', 'L00132', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('881', 'L00133', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('882', 'L00134', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('883', 'L00135', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('884', 'L00136', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('885', 'L00137', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('886', 'L00138', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('887', 'L00139', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('888', 'L00140', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('889', 'L00141', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('890', 'L00142', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('891', 'L00143', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('892', 'L00144', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('893', 'L00145', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('894', 'L00146', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('895', 'L00147', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('896', 'L00148', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('897', 'L00149', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('898', 'L00150', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('899', 'L00151', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('900', 'L00152', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('901', 'L00153', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('902', 'L00154', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('903', 'L00155', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('904', 'L00156', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('905', 'L00157', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('906', 'L00158', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('907', 'L00159', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('908', 'L00160', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('909', 'L00161', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('910', 'L00162', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('911', 'L00163', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('912', 'L00164', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('913', 'L00165', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('914', 'L00166', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('915', 'L00167', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('916', 'L00168', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('917', 'L00169', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('918', 'L00170', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('919', 'L00171', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('920', 'L00172', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('921', 'L00173', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('922', 'L00174', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('923', 'L00175', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('924', 'L00176', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('925', 'L00177', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('926', 'L00178', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('927', 'L00179', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('928', 'L00180', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('929', 'L00181', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('930', 'L00182', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('931', 'L00183', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('932', 'L00184', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('933', 'L00185', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('934', 'L00186', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('935', 'L00187', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('936', 'L00188', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('937', 'L00189', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('938', 'L00190', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('939', 'L00191', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('940', 'L00192', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('941', 'L00193', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('942', 'L00194', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('943', 'L00195', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('944', 'L00196', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('945', 'L00197', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('946', 'L00198', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('947', 'L00199', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('948', 'L00200', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('949', 'L00201', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('950', 'L00202', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('951', 'L00203', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('952', 'L00204', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('953', 'L00205', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('954', 'L00206', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('955', 'L00207', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('956', 'L00208', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('957', 'L00209', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('958', 'L00210', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('959', 'L00211', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('960', 'L00212', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('961', 'L00213', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('962', 'L00214', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('963', 'L00215', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('964', 'L00216', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('965', 'L00217', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('966', 'L00218', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('967', 'L00219', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('968', 'L00220', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('969', 'L00221', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('970', 'L00222', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('971', 'L00223', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('972', 'L00224', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('973', 'L00225', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('974', 'L00226', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('975', 'L00227', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('976', 'L00228', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('977', 'L00229', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('978', 'L00230', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('979', 'L00231', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('980', 'L00232', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('981', 'L00233', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('982', 'L00234', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('983', 'L00235', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('984', 'L00236', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('985', 'L00237', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('986', 'L00238', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('987', 'L00239', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('988', 'L00240', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('989', 'L00241', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('990', 'L00242', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('991', 'L00243', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('992', 'L00244', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('993', 'L00245', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('994', 'L00246', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('995', 'L00247', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('996', 'L00248', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('997', 'L00249', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('998', 'L00250', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('999', 'L00251', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1000', 'L00252', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1001', 'L00253', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1002', 'L00254', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1003', 'L00255', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1004', 'L00256', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1005', 'L00257', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1006', 'L00258', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1007', 'L00259', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1008', 'L00260', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1009', 'L00261', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1010', 'L00262', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1011', 'L00263', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1012', 'L00264', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1013', 'L00265', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1014', 'L00266', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1015', 'L00267', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1016', 'L00268', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1017', 'L00269', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1018', 'L00270', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1019', 'L00271', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1020', 'L00272', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1021', 'L00273', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1022', 'L00274', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1023', 'L00275', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1024', 'L00276', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1025', 'L00277', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1026', 'L00278', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1027', 'L00279', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1028', 'L00280', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1029', 'L00281', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1030', 'L00282', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1031', 'L00283', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1032', 'L00284', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1033', 'L00285', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1034', 'L00286', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1035', 'L00287', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1036', 'L00288', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1037', 'L00289', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1038', 'L00290', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1039', 'L00291', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1040', 'L00292', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1041', 'L00293', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1042', 'L00294', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1043', 'L00295', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1044', 'L00296', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1045', 'L00297', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1046', 'L00298', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1047', 'L00299', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1048', 'L00300', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1049', 'L00301', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1050', 'L00302', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1051', 'L00303', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1052', 'L00304', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1053', 'L00305', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1054', 'L00306', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1055', 'L00307', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1056', 'L00308', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1057', 'L00309', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1058', 'L00310', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1059', 'L00311', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1060', 'L00312', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1061', 'L00313', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1062', 'L00314', 'L', 'empty', '0', 'XT0001', null, 'xqs', null, '');
INSERT INTO `wcscontainer` VALUES ('1063', 'M00302', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1064', 'M00303', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1065', 'M00304', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1066', 'M00305', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1067', 'M00306', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1068', 'M00307', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1069', 'M00308', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1070', 'M00309', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1071', 'M00310', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1072', 'M00311', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1073', 'M00312', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1074', 'M00313', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1075', 'M00314', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1076', 'M00315', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1077', 'M00316', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1078', 'M00317', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1079', 'M00318', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1080', 'M00319', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1081', 'M00320', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1082', 'M00321', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1083', 'M00322', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1084', 'M00323', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1085', 'M00324', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1086', 'M00325', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1087', 'M00326', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1088', 'M00327', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1089', 'M00328', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1090', 'M00329', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1091', 'M00330', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1092', 'M00331', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1093', 'M00332', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1094', 'M00333', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1095', 'M00334', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1096', 'M00335', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1097', 'M00336', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1098', 'M00337', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1099', 'M00338', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1100', 'M00339', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1101', 'M00340', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1102', 'M00341', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1103', 'M00342', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1104', 'M00343', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1105', 'M00344', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1106', 'M00345', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1107', 'M00346', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1108', 'M00347', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1109', 'M00348', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1110', 'M00349', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1111', 'M00350', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1112', 'M00351', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1113', 'M00352', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1114', 'M00353', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1115', 'M00354', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1116', 'M00355', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1117', 'M00356', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1118', 'M00357', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1119', 'M00358', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1120', 'M00359', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1121', 'M00360', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1122', 'M00361', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1123', 'M00362', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1124', 'M00363', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1125', 'M00364', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1126', 'M00365', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1127', 'M00366', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1128', 'M00367', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1129', 'M00368', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1130', 'M00369', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1131', 'M00370', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1132', 'M00371', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1133', 'M00372', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1134', 'M00373', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1135', 'M00374', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1136', 'M00375', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1137', 'M00376', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1138', 'M00377', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1139', 'M00378', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1140', 'M00379', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1141', 'M00380', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1142', 'M00381', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1143', 'M00382', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1144', 'M00383', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1145', 'M00384', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1146', 'M00385', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1147', 'M00386', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1148', 'M00387', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1149', 'M00388', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1150', 'M00389', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1151', 'M00390', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1152', 'M00391', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1153', 'M00392', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1154', 'M00393', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1155', 'M00394', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1156', 'M00395', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1157', 'M00396', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1158', 'M00397', 'M', 'empty', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1159', 'M00398', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1160', 'M00399', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1161', 'M00400', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1162', 'M00401', 'M', 'some', '0', 'XT0001', null, 'tony', null, '');
INSERT INTO `wcscontainer` VALUES ('1163', 'S00012', 'S', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1164', 'S00013', 'S', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1165', 'S00014', 'S', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1166', 'S00015', 'S', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1167', 'S00016', 'S', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1168', 'L00315', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, 'wjj');
INSERT INTO `wcscontainer` VALUES ('1169', 'L00316', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, 'wjj');
INSERT INTO `wcscontainer` VALUES ('1170', 'L00317', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, 'wjj');
INSERT INTO `wcscontainer` VALUES ('1171', 'L00318', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, 'wjj');
INSERT INTO `wcscontainer` VALUES ('1172', 'L00319', 'L', 'empty', '0', 'XT0001', null, 'wjj', null, 'wjj');
INSERT INTO `wcscontainer` VALUES ('1173', 'S00017', 'S', 'empty', '0', 'XT0001', null, 'wjj', null, 'wjj');
INSERT INTO `wcscontainer` VALUES ('1174', 'M00402', 'M', 'some', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1175', 'L00320', 'L', 'some', '0', 'XT0001', null, 'ricard', null, '');
INSERT INTO `wcscontainer` VALUES ('1182', 'S00018', 'S', 'some', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1183', 'S00019', 'S', 'some', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1184', 'S00020', 'S', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1185', 'S00021', 'S', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1186', 'S00022', 'S', 'empty', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1187', 'M00403', 'M', 'some', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1188', 'M00404', 'M', 'some', '0', 'XT0001', null, 'wjj', null, '');
INSERT INTO `wcscontainer` VALUES ('1189', 'M00405', 'M', 'some', '0', 'XT0001', null, 'wjj', null, '');

-- ----------------------------
-- Table structure for wcscontentlog
-- ----------------------------
DROP TABLE IF EXISTS `wcscontentlog`;
CREATE TABLE `wcscontentlog` (
  `id` int(11) NOT NULL,
  `title` varchar(255) DEFAULT NULL,
  `content` varchar(1000) DEFAULT NULL,
  `flag` varchar(255) DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) DEFAULT '' COMMENT '创建人',
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) DEFAULT '' COMMENT '最后更新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of wcscontentlog
-- ----------------------------

-- ----------------------------
-- Table structure for wcsdict
-- ----------------------------
DROP TABLE IF EXISTS `wcsdict`;
CREATE TABLE `wcsdict` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) COLLATE utf8_bin NOT NULL,
  `name` varchar(50) COLLATE utf8_bin NOT NULL,
  `remark` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsdict
-- ----------------------------
INSERT INTO `wcsdict` VALUES ('1', 'TaskType', '任务类型', null, null, null, null, null);
INSERT INTO `wcsdict` VALUES ('2', 'menuType', '菜单类型', null, '2018-11-01 15:32:45', 'admin', null, null);
INSERT INTO `wcsdict` VALUES ('3', 'TaskStatus', '任务状态', null, null, null, null, null);
INSERT INTO `wcsdict` VALUES ('4', 'LocationStatus', '库位状态', null, null, null, null, null);
INSERT INTO `wcsdict` VALUES ('5', 'TaskPriority', '任务优先级', null, null, null, null, null);
INSERT INTO `wcsdict` VALUES ('7', 'RemoteUrls', '远程接口访问地址', null, '2019-01-07 10:58:28', 'admin', null, null);
INSERT INTO `wcsdict` VALUES ('9', 'Port', '出入口', '出入口', '2019-10-12 14:31:02', 'admin', null, null);

-- ----------------------------
-- Table structure for wcsdictdetail
-- ----------------------------
DROP TABLE IF EXISTS `wcsdictdetail`;
CREATE TABLE `wcsdictdetail` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `headId` int(11) NOT NULL,
  `code` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `name` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `value` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `sort` int(11) NOT NULL,
  `remark` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsdictdetail
-- ----------------------------
INSERT INTO `wcsdictdetail` VALUES ('1', '1', '100', '整盘入库', '100', '0', null, null, null, '2018-10-27 14:08:07', 'admin');
INSERT INTO `wcsdictdetail` VALUES ('6', '1', '300', '整盘出库', '300', '0', null, '2018-10-27 14:01:09', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('7', '2', 'menu', '菜单', 'menu', '0', null, '2018-11-01 15:34:11', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('8', '2', 'catalog', '目录', 'catalog', '0', null, '2018-11-01 15:34:58', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('9', '2', 'button', '按钮', 'button', '0', null, '2018-11-01 15:35:15', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('10', '3', '1', '生成任务', '1', '0', null, null, null, null, null);
INSERT INTO `wcsdictdetail` VALUES ('11', '3', '10', '下达任务', '10', '0', null, null, null, null, null);
INSERT INTO `wcsdictdetail` VALUES ('12', '3', '20', '下发堆垛机库内取货任务', '20', '0', null, null, null, '2019-01-07 10:49:46', 'admin');
INSERT INTO `wcsdictdetail` VALUES ('15', '4', 'disable', '禁用', '30', '0', null, null, null, '2019-09-03 16:53:55', 'admin');
INSERT INTO `wcsdictdetail` VALUES ('16', '4', 'idle', '空闲', '0', '0', null, null, null, '2019-09-03 16:54:04', 'admin');
INSERT INTO `wcsdictdetail` VALUES ('18', '4', 'lock', '预定', '20', '0', null, null, null, '2019-09-03 16:54:18', 'admin');
INSERT INTO `wcsdictdetail` VALUES ('21', '1', '500', '空容器入库', '500', '0', null, '2019-01-07 10:47:43', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('22', '1', '600', '空容器出库', '600', '0', null, '2019-01-07 10:48:02', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('23', '1', '200', '补充入库', '200', '0', null, '2019-01-07 10:48:17', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('24', '1', '400', '分拣出库', '400', '0', null, '2019-01-07 10:48:30', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('25', '1', '700', '盘点', '700', '0', null, '2019-01-07 10:48:47', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('26', '1', '800', '移库', '800', '0', null, '2019-01-07 10:49:00', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('27', '1', '900', '出库查看', '900', '0', null, '2019-01-07 10:49:13', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('28', '3', '25', '响应堆垛机库内取货任务完成', '25', '0', null, '2019-01-07 10:50:13', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('29', '3', '30', '下发堆垛机库外放货任务', '30', '0', null, '2019-01-07 10:50:24', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('30', '3', '35', '响应堆垛机库外放货任务完成', '35', '0', null, '2019-01-07 10:50:34', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('31', '3', '40', '响应接出口站台地址请求', '40', '0', null, '2019-01-07 10:50:46', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('32', '3', '50', '到达拣选站台', '50', '0', null, '2019-01-07 10:50:58', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('33', '3', '60', '拣选台回库', '60', '0', null, '2019-01-07 10:51:09', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('34', '3', '70', '响应接入口位置到达', '70', '0', null, '2019-01-07 10:51:29', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('35', '3', '75', '下发堆垛机库外取货任务', '75', '0', null, '2019-01-07 10:51:39', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('36', '3', '80', '响应堆垛机库外取货任务完成', '80', '0', null, '2019-01-07 10:51:48', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('37', '3', '85', '下发堆垛机库内放货任务', '85', '0', null, '2019-01-07 10:51:58', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('38', '3', '90', '响应堆垛机库内放货任务完成', '90', '0', null, '2019-01-07 10:52:10', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('39', '3', '100', '任务完成', '100', '0', null, '2019-01-07 10:52:22', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('41', '5', '1', '1', '1', '0', null, '2019-01-07 10:54:59', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('42', '5', '2', '2', '2', '0', null, '2019-01-07 10:55:14', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('43', '7', 'TaskComplete', '任务完成接口', 'wms/task/task/complete', '0', null, '2019-01-07 10:59:16', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('44', '7', 'TaskExecute', '任务下发接口', 'wms/task/task/execute', '0', null, '2019-01-07 10:59:39', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('45', '7', 'Login', '登录接口', 'wms/login', '0', null, '2019-01-07 10:59:57', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('46', '7', 'Heartbeat', '心跳接口', 'wms/mobile/heartbeat', '0', null, '2019-01-07 11:00:14', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('47', '7', 'HandleEmptyOut', '空出处理接口', 'wms/task/task/handleEmptyOut', '0', null, '2019-01-07 11:00:33', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('48', '7', 'GetLocation', '获取去向库位接口', 'wms/task/task/setLocationCode', '0', null, '2019-01-07 11:00:54', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('49', '7', 'HandleForkError', '取货错误接口', 'wms/task/task/createTransfer', '0', null, '2019-01-07 11:01:19', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('51', '1', '1000', '换站', '1000', '0', null, '2019-10-12 14:18:43', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('52', '9', '1000', 'A1', null, '0', 'A1入库口', '2019-10-12 14:33:53', 'admin', '2019-11-04 14:56:33', 'admin');
INSERT INTO `wcsdictdetail` VALUES ('53', '9', '1012', 'A2', null, '0', '出口', '2019-10-12 14:35:18', 'admin', '2019-11-04 14:56:44', 'admin');
INSERT INTO `wcsdictdetail` VALUES ('54', '3', '120', '任务回传成功', '120', '10', null, '2019-11-11 13:41:10', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('55', '3', '110', '任务回传失败', '110', '0', null, '2019-11-11 13:41:51', 'admin', null, null);
INSERT INTO `wcsdictdetail` VALUES ('56', '3', '130', '任务回传异常', '130', '0', null, '2019-11-11 13:47:38', 'admin', '2019-11-11 13:47:58', 'admin');

-- ----------------------------
-- Table structure for equipment
-- ----------------------------
DROP TABLE IF EXISTS `equipment`;
CREATE TABLE `equipment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) COLLATE utf8_bin NOT NULL,
  `name` varchar(50) COLLATE utf8_bin NOT NULL,
  `equipmentTypeId` int(11) NOT NULL,
  `ip` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT 'plc的ip',
  `ledIp` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT 'led控制显示屏',
  `scanIp` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '扫描头IP',
  `roadWay` int(50) DEFAULT NULL COMMENT '巷道',
  `basePlcDB` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '设备对应PLC的基数地址',
  `selfAddress` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '自身地址',
  `backAddress` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '回退地址',
  `goAddress` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '前进地址',
  `stationIndex` int(11) DEFAULT NULL COMMENT '站台索引',
  `rowIndex` int(11) NOT NULL DEFAULT '0' COMMENT '排索引，用于确认左右出叉',
  `columnIndex` int(11) NOT NULL DEFAULT '0' COMMENT '列索引',
  `layerIndex` int(11) NOT NULL DEFAULT '0' COMMENT '层索引',
  `connectName` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `groupName` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `description` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `warehouseCode` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `disable` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of equipment
-- ----------------------------
INSERT INTO `equipment` VALUES ('13', 'SRM1', '1号堆垛机', '3', '192.168.10.10', null, null, '1', null, 'SRM1', null, null, null, '0', '0', '0', null, null, null, 'XT0001', '2019-10-18 09:19:35', 'admin', null, null, '0');
INSERT INTO `equipment` VALUES ('15', 'Port1', '出口1', '7', '192.168.10.21', null, null, '0', 'D3000', '1000', '1000', '1004', null, '0', '0', '0', null, null, null, 'XT0001', '2019-10-18 17:18:16', 'admin', null, null, '0');
INSERT INTO `equipment` VALUES ('16', 'Port2', '出口2', '7', null, null, null, '0', 'D3001', '1012', '1012', '1014', null, '0', '0', '0', null, null, null, 'XT0001', '2019-11-04 14:00:49', 'admin', null, null, '0');

-- ----------------------------
-- Table structure for equipment_prop
-- ----------------------------
DROP TABLE IF EXISTS `equipment_prop`;
CREATE TABLE `equipment_prop` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `equipmentId` int(11) NOT NULL,
  `equipmentTypePropTemplateId` int(11) NOT NULL,
  `equipmentTypePropTemplateCode` varchar(50) COLLATE utf8_bin NOT NULL,
  `serverHandle` int(11) DEFAULT NULL,
  `address` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `value` varchar(50) COLLATE utf8_bin DEFAULT '0',
  `remark` varchar(200) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE KEY `id` (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=831 DEFAULT CHARSET=utf8 COLLATE=utf8_bin ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of equipment_prop
-- ----------------------------
INSERT INTO `equipment_prop` VALUES ('625', '13', '9', 'Number', '0', 'DB101W0', '', '堆垛机编号', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('626', '13', '10', 'OperationModel', '0', 'DB101W2', '', '操作模式', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('627', '13', '11', 'HeartBeat', '0', 'DB101W4', '', '心跳', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('628', '13', '12', 'TaskLimit', '0', 'DB101W6', '', '任务限制', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('629', '13', '13', 'Fork1TaskExcuteStatus', '0', 'DB101W42', '', '货叉1_任务执行', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('630', '13', '14', 'Fork1TaskNo', '0', 'DB101D46', '', '货叉1_任务号', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('631', '13', '15', 'Fork1TaskType', '0', 'DB101W54', '', '货叉1_任务类型', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('632', '13', '16', 'HorizontalDistance', '0', 'DB101D8', '', '水平测距', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('633', '13', '17', 'VerticalDistance', '0', 'DB101D12', '', '起升测距', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('634', '13', '18', 'For1kDistance', '0', 'DB101D16', '', '货叉1伸叉测距', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('635', '13', '19', 'CurrentColumn', '0', 'DB101W24', '', '当前列', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('636', '13', '20', 'CurrentLayer', '0', 'DB101W26', '', '当前层', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('637', '13', '21', 'CurrentStation', '0', 'DB101W28', '', '当前出/入口', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('638', '13', '22', 'Fork1FrontOut', '0', 'DB101X30.0', '', '货叉1_是否货物前超', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('639', '13', '23', 'Fork1BehindOut', '0', 'DB101X30.1', '', '货叉1_是否货物后超', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('640', '13', '24', 'Fork1LeftForkOut', '0', 'DB101X30.2', '', '货叉1_是否左侧外形超限', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('641', '13', '25', 'Fork1RightForkOut', '0', 'DB101X30.3', '', '货叉1_是否右侧外形超限', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('642', '13', '26', 'Fork1OverHeight1', '0', 'DB101X30.4', '', '货叉1_超高1', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('643', '13', '27', 'Fork1OverHeight2', '0', 'DB101X30.5', '', '货叉1_超高2', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('644', '13', '28', 'Fork1OverHeight3', '0', 'DB101X30.6', '', '货叉1_超高3', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('645', '13', '29', 'Fork1OverHeight', '0', 'DB101X30.7', '', '货叉1_货物超高', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('646', '13', '30', 'Fork1PalletForkTimeout', '0', 'DB101X31.0', '', '货叉1_货叉超时', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('647', '13', '31', 'Fork1LeftLimitAlarm', '0', 'DB101X31.1', '', '货叉1_是否左侧极限报警', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('648', '13', '32', 'Fork1RightLimitAlarm', '0', 'DB101X31.2', '', '货叉1_是否右侧极限报警', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('649', '13', '33', 'Fork1ForkUuivertor', '0', 'DB101X31.3', '', '货叉1_货叉变频器故障', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('650', '13', '34', 'Fork1ForkBreakerOrCocontactor', '0', 'DB101X31.4', '', '货叉1_货叉断路器/接触器故障', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('651', '13', '35', 'Fork1GoodsInspectionSensor', '0', 'DB101X31.5', '', '货叉1_是否货物检测传感器故障', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('652', '13', '36', 'Fork1ForkAlignmentSensor', '0', 'DB101X31.6', '', '货叉1_是否货叉定位传感器故障', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('653', '13', '37', 'Fork1DirectionError', '0', 'DB101X31.7', '', '货叉1_是否运行方向错误', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('654', '13', '38', 'Fork1XYForkExcute', '0', 'DB101X32.0', '', '货叉1_是否货叉执行动作错误', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('655', '13', '39', 'Fork1SetValueError', '0', 'DB101X32.1', '', '货叉1_是否设定值错误', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('656', '13', '40', 'Fork1PickupTaskError', '0', 'DB101X32.2', '', '货叉1_是否取货任务错误', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('657', '13', '41', 'Fork1Spare3', '0', 'DB101X32.3', '', '货叉1_Spare3', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('658', '13', '42', 'Fork1DoubleIn', '0', 'DB101X32.4', '', '货叉1_双重入库', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('659', '13', '43', 'Fork1EmptyOut', '0', 'DB101X32.5', '', '货叉1_是否空货位出库', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('660', '13', '44', 'Fork1ForkHasPallet', '0', 'DB101X32.6', '', '货叉1_是否货叉有货', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('661', '13', '45', 'Fork1ForkError', '0', 'DB101X32.7', '', '货叉1_货叉总故障', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('662', '13', '46', 'Fork1Spare4', '0', 'DB101B33', '', '货叉1_Spare4', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('663', '13', '47', 'Overload', '0', 'DB101X38.0', '', '是否过载', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('664', '13', '48', 'Rope', '0', 'DB101X38.1', '', '是否松绳', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('665', '13', '49', 'RunningUuivertorAlarm', '0', 'DB101X38.2', '', '是否行走变频器报警', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('666', '13', '50', 'RaisingUuivertorAlarm', '0', 'DB101X38.3', '', '是否起升变频器报警', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('667', '13', '51', 'RunningTimeout', '0', 'DB101X38.4', '', '是否运行超时', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('668', '13', '52', 'RaisingTimeout', '0', 'DB101X38.5', '', '是否起升超时', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('669', '13', '53', 'HorizontalLaserDataError', '0', 'DB101X38.6', '', '是否水平激光数据错误', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('670', '13', '54', 'RaisingBarcodeDataError', '0', 'DB101X38.7', '', '起升条码数据错误', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('671', '13', '55', 'AdressError', '0', 'DB101X39.0', '', '是否地址错', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('672', '13', '56', 'MainCocontactorInterrupt', '0', 'DB101X39.1', '', '主接触器断开', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('673', '13', '57', 'HorizontalBreakerOrBrakeInterrupt', '0', 'DB101X39.2', '', '水平断路器/制动器跳闸', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('674', '13', '58', 'RaisingBreakerOrBrakeInterrupt', '0', 'DB101X39.3', '', '是否起升断路器/制动器跳闸', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('675', '13', '59', 'HorizontalLeadingendOut', '0', 'DB101X39.4', '', '是否水平前端超限（前进终点）', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('676', '13', '60', 'HorizontalTrailingendOut', '0', 'DB101X39.5', '', '是否水平后端超限（后退终点）', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('677', '13', '61', 'VerticalHorizontalLeadingendOut', '0', 'DB101X39.6', '', '是否垂直上端超限（上升终点）', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('678', '13', '62', 'VerticalHorizontalTrailingendOut', '0', 'DB101X39.7', '', '垂直下端超限（下降终点）', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('679', '13', '63', 'HorizontalUuivertorSpeed', '0', 'DB101X40.0', '', '是否水平变频器速度超过设定值', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('680', '13', '64', 'RaisingUuivertorSpeed', '0', 'DB101X40.1', '', '是否起升变频器速度超过设定值', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('681', '13', '149', 'WCSForkAction', '0', 'DB100W0', '', '货叉动作类型', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('682', '13', '150', 'WCSFork1TaskFlag', '0', 'DB100W2', '', '货叉1_任务标志', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('683', '13', '151', 'WCSFork1Row', '0', 'DB100W4', '', '货叉1_取放货地址:  行', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('684', '13', '152', 'WCSFork1Column', '0', 'DB100W6', '', '货叉1_取放货列', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('685', '13', '153', 'WCSFork1Layer', '0', 'DB100W8', '', '货叉1_取放货层', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('686', '13', '154', 'WCSFork1Station', '0', 'DB100W10', '', '货叉1_取放货出入口', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('687', '13', '155', 'WCSFork1Task', '0', 'DB100D22', '', '货叉1_任务号', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('688', '13', '156', 'WCSHeartBeat', '0', 'DB100W36', '', '心跳', '2019-10-18 09:19:46', '', null, null);
INSERT INTO `equipment_prop` VALUES ('771', '15', '276', 'RequestMessage', '0', 'D3000W0', '', '地址请求', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('772', '15', '277', 'RequestLoadStatus', '0', 'D3000W2', '', '地址请求-装载状态', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('773', '15', '278', 'RequestNumber', '0', 'D3000W4', '', '地址请求-读码器编号', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('774', '15', '279', 'RequestBarcode', '0', 'D3000CHAR6,20', '', '地址请求-条码', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('775', '15', '280', 'RequestWeight', '0', 'D3000W26', '', '地址请求-货物重量', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('776', '15', '281', 'RequestLength', '0', 'D3000W28', '', '地址请求-货物长度', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('777', '15', '282', 'RequestWidth', '0', 'D3000W30', '', '地址请求-货物宽度', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('778', '15', '283', 'RequestHeight', '0', 'D3000W32', '', '地址请求-货物高度', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('779', '15', '284', 'RequestRetCode', '0', 'D3000W34', '', '地址请求-RetCode', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('780', '15', '285', 'RequestBackup', '0', 'D3000W36', '', '地址请求-备用', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('781', '15', '286', 'ArriveMessage', '0', 'D3000W40', '', '位置到达-报文', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('782', '15', '287', 'ArriveResult', '0', 'D3000W42', '', '位置到达-结果', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('783', '15', '288', 'ArriveRealAddress', '0', 'D3000W46', '', '位置到达-实际到达地址', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('784', '15', '289', 'ArriveAllcationAddress', '0', 'D3000W44', '', '位置到达-WCS分配地址', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('785', '15', '290', 'ArriveBarcode', '0', 'D3000CHAR48,20', '', '位置到达-条码', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('786', '15', '291', 'ArrivePaddingBit', '0', 'D3000W68', '', '位置到达-填充位', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('787', '15', '300', 'WCSReplyMessage', '0', 'D3000W0', '', 'WCS地址回复报文', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('788', '15', '301', 'WCSReplyLoadStatus', '0', 'D3000W2', '', 'WCS地址回复-装载状态', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('789', '15', '302', 'WCSReplyNumber', '0', 'D3000W4', '', 'WCS地址回复-读码器编码', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('790', '15', '303', 'WCSReplyBarcode', '0', 'D3000CHAR6,20', '', 'WCS地址回复-条码', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('791', '15', '304', 'WCSReplyWeight', '0', 'D3000W26', '', 'WCS地址回复-货物重量', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('792', '15', '305', 'WCSReplyLength', '0', 'D3000W28', '', 'WCS地址回复-货物长度', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('793', '15', '306', 'WCSReplyWidth', '0', 'D3000W30', '', 'WCS地址回复-货物宽度', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('794', '15', '307', 'WCSReplyHeight', '0', 'D3000W32', '', 'WCS地址回复-货物高度', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('795', '15', '308', 'WCSReplyAddress', '0', 'D3000W34', '', 'WCS地址回复-目标地址', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('796', '15', '309', 'WCSReplyBackUp', '0', 'D3000W36', '', 'WCS地址回复-备用', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('797', '15', '314', 'WCSACKMessage', '0', 'D3000W40', '', 'WCSACK报文', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('798', '15', '315', 'WCSACKLoadStatus', '0', 'D3000W42', '', 'WCSACK-装载状态', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('799', '15', '316', 'WCSACKNumber', '0', 'D3000W44', '', 'WCSACK-读码器编码', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('800', '15', '317', 'WCSACKBackup', '0', 'D3000W46', '', 'WCSACK-备用', '2019-11-04 13:59:20', '', null, null);
INSERT INTO `equipment_prop` VALUES ('801', '16', '276', 'RequestMessage', '0', 'D3001W0', '', '地址请求', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('802', '16', '277', 'RequestLoadStatus', '0', 'D3001W2', '', '地址请求-装载状态', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('803', '16', '278', 'RequestNumber', '0', 'D3001W4', '', '地址请求-读码器编号', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('804', '16', '279', 'RequestBarcode', '0', 'D3001CHAR6,20', '', '地址请求-条码', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('805', '16', '280', 'RequestWeight', '0', 'D3001W26', '', '地址请求-货物重量', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('806', '16', '281', 'RequestLength', '0', 'D3001W28', '', '地址请求-货物长度', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('807', '16', '282', 'RequestWidth', '0', 'D3001W30', '', '地址请求-货物宽度', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('808', '16', '283', 'RequestHeight', '0', 'D3001W32', '', '地址请求-货物高度', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('809', '16', '284', 'RequestRetCode', '0', 'D3001W34', '', '地址请求-RetCode', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('810', '16', '285', 'RequestBackup', '0', 'D3001W36', '', '地址请求-备用', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('811', '16', '286', 'ArriveMessage', '0', 'D3001W40', '', '位置到达-报文', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('812', '16', '287', 'ArriveResult', '0', 'D3001W42', '', '位置到达-结果', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('813', '16', '288', 'ArriveRealAddress', '0', 'D3001W46', '', '位置到达-实际到达地址', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('814', '16', '289', 'ArriveAllcationAddress', '0', 'D3001W44', '', '位置到达-WCS分配地址', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('815', '16', '290', 'ArriveBarcode', '0', 'D3001CHAR48,20', '', '位置到达-条码', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('816', '16', '291', 'ArrivePaddingBit', '0', 'D3001W68', '', '位置到达-填充位', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('817', '16', '300', 'WCSReplyMessage', '0', 'D3001W0', '', 'WCS地址回复报文', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('818', '16', '301', 'WCSReplyLoadStatus', '0', 'D3001W2', '', 'WCS地址回复-装载状态', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('819', '16', '302', 'WCSReplyNumber', '0', 'D3001W4', '', 'WCS地址回复-读码器编码', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('820', '16', '303', 'WCSReplyBarcode', '0', 'D3001CHAR6,20', '', 'WCS地址回复-条码', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('821', '16', '304', 'WCSReplyWeight', '0', 'D3001W26', '', 'WCS地址回复-货物重量', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('822', '16', '305', 'WCSReplyLength', '0', 'D3001W28', '', 'WCS地址回复-货物长度', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('823', '16', '306', 'WCSReplyWidth', '0', 'D3001W30', '', 'WCS地址回复-货物宽度', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('824', '16', '307', 'WCSReplyHeight', '0', 'D3001W32', '', 'WCS地址回复-货物高度', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('825', '16', '308', 'WCSReplyAddress', '0', 'D3001W34', '', 'WCS地址回复-目标地址', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('826', '16', '309', 'WCSReplyBackUp', '0', 'D3001W36', '', 'WCS地址回复-备用', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('827', '16', '314', 'WCSACKMessage', '0', 'D3001W40', '', 'WCSACK报文', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('828', '16', '315', 'WCSACKLoadStatus', '0', 'D3001W42', '', 'WCSACK-装载状态', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('829', '16', '316', 'WCSACKNumber', '0', 'D3001W44', '', 'WCSACK-读码器编码', '2019-11-04 16:34:48', '', null, null);
INSERT INTO `equipment_prop` VALUES ('830', '16', '317', 'WCSACKBackup', '0', 'D3001W46', '', 'WCSACK-备用', '2019-11-04 16:34:48', '', null, null);

-- ----------------------------
-- Table structure for equipment_prop2
-- ----------------------------
DROP TABLE IF EXISTS `equipment_prop2`;
CREATE TABLE `equipment_prop2` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `equipmentId` int(11) NOT NULL,
  `equipmentTypePropTemplateId` int(11) NOT NULL,
  `equipmentTypePropTemplateCode` varchar(50) COLLATE utf8_bin NOT NULL,
  `serverHandle` int(11) DEFAULT NULL,
  `address` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `value` varchar(50) COLLATE utf8_bin DEFAULT '0',
  `remark` varchar(200) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=875 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of equipment_prop2
-- ----------------------------
INSERT INTO `equipment_prop2` VALUES ('108', '2', '9', 'Number', '0', 'DB101.DBW0', '', '堆垛机PLC编号', '2018-11-14 14:53:53', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('109', '2', '10', 'OperationModel', '0', 'DB101.DBW2', '', '操作模式：1-维修；2-手动；3-机载操作；4-单机自 动；5-联机', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('110', '2', '11', 'HeartBeat', '0', 'DB101.DBW4', '', '心跳', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('111', '2', '12', 'TaskLimit', '0', 'DB101.DBW6', '', '任务限制：1-无限制，2-限制入库，3-限制出库，4-拣选 ', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('112', '2', '13', 'Fork1TaskExcuteStatus', '0', 'DB101.DBW42', '', '1-待机；2-任务执行中；3-任务完成；4-任务中断（出 错，空出，满入）；5-下发任务错误', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('113', '2', '14', 'Fork1TaskNo', '0', 'DB101.DBD46', '', '任务号1', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('114', '2', '15', 'Fork1TaskType', '0', 'DB101.DBW54', '', '货叉1任务类型：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('115', '2', '16', 'HorizontalDistance', '0', 'DB101.DBD8', '', '水平测距数据', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('116', '2', '17', 'VerticalDistance', '0', 'DB101.DBD12', '', '起升测距数据', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('117', '2', '18', 'ForkDistance', '0', 'DB101.DBD16', '', '货叉的距离', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('118', '2', '19', 'CurrentColumn', '0', 'DB101.DBW24', '', '当前列', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('119', '2', '20', 'CurrentLayer', '0', 'DB101.DBW26', '', '当前层', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('120', '2', '21', 'CurrentStation', '0', 'DB101.DBW28', '', '当前站台1-10', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('121', '2', '22', 'Fork1FrontOut', '0', 'DB101.DBX30.0', '', '前超', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('122', '2', '23', 'Fork1BehindOut', '0', 'DB101.DBX30.1', '', '后超', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('123', '2', '24', 'Fork1LeftForkOut', '0', 'DB101.DBX30.2', '', '左超', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('124', '2', '25', 'Fork1RightForkOut', '0', 'DB101.DBX30.3', '', '右超', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('125', '2', '26', 'Fork1OverHeight1', '0', 'DB101.DBX30.4', '', '超高1（货位高度和送货地址不匹配）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('126', '2', '27', 'Fork1OverHeight2', '0', 'DB101.DBX30.5', '', '超高2（货位高度和送货地址不匹配）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('127', '2', '28', 'Fork1OverHeight3', '0', 'DB101.DBX30.6', '', '超高3（货位高度和送货地址不匹配）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('128', '2', '29', 'Fork1OverHeight', '0', 'DB101.DBX30.7', '', '货位超高（货位高度和送货地址不匹配）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('129', '2', '30', 'Fork1PalletForkTimeout', '0', 'DB101.DBX31.0', '', '0-无超时；1-货叉超时', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('130', '2', '31', 'Fork1LeftLimitAlarm', '0', 'DB101.DBX31.1', '', '0-无故障；1-左侧极限报警', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('131', '2', '32', 'Fork1RightLimitAlarm', '0', 'DB101.DBX31.2', '', '0-无故障；1-右侧极限报警', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('132', '2', '33', 'Fork1ForkUuivertor', '0', 'DB101.DBX31.3', '', '0-无故障；1-货叉变频器故障', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('133', '2', '34', 'Fork1ForkBreakerOrCocontactor', '0', 'DB101.DBX31.4', '', '0-无故障；1-货叉断路器/接触器故障', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('134', '2', '35', 'Fork1GoodsInspectionSensor', '0', 'DB101.DBX31.5', '', '0-无故障；1-货物检测传感器故障', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('135', '2', '36', 'Fork1ForkAlignmentSensor', '0', 'DB101.DBX31.6', '', '0-无故障；1-货叉定位传感器故障', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('136', '2', '37', 'Fork1DirectionError', '0', 'DB101.DBX31.7', '', '0-无故障；1-运行方向错误', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('137', '2', '38', 'Fork1XYForkExcute', '0', 'DB101.DBX32.0', '', '0-无故障；1-X轴、Y轴、货叉执行动作错误', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('138', '2', '39', 'Fork1SetValueError', '0', 'DB101.DBX32.1', '', '', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('139', '2', '40', 'Fork1PickupTaskError', '0', 'DB101.DBX32.2', '', '', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('140', '2', '41', 'Fork1Spare3', '0', 'DB101.DBX32.3', '', 'Spare', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('141', '2', '42', 'Fork1DoubleIn', '0', 'DB101.DBX32.4', '', '0-无故障；1-双重入库（满入）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('142', '2', '43', 'Fork1EmptyOut', '0', 'DB101.DBX32.5', '', '0-无故障；1-空货位出库（空出）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('143', '2', '44', 'Fork1ForkHasPallet', '0', 'DB101.DBX32.6', '', '0-货叉无货 1-货叉有货', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('144', '2', '45', 'Fork1ForkError', '0', 'DB101.DBX32.7', '', '0-无故障；1-货叉故障', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('145', '2', '46', 'Fork1Spare4', '0', 'DB101.DBB33', '', 'Spare', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('146', '2', '47', 'Overload', '0', 'DB101.DBX38.0', '', '0-无过载；1-堆垛机故障', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('147', '2', '48', 'Rope', '0', 'DB101.DBX38.1', '', '0-无松绳；1-松绳', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('148', '2', '49', 'RunningUuivertorAlarm', '0', 'DB101.DBX38.2', '', '0-无报警；1-行走变频器报警', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('149', '2', '50', 'RaisingUuivertorAlarm', '0', 'DB101.DBX38.3', '', '0-无报警；1-起升变频器报警', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('150', '2', '51', 'RunningTimeout', '0', 'DB101.DBX38.4', '', '0-无超时；1-运行超时', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('151', '2', '52', 'RaisingTimeout', '0', 'DB101.DBX38.5', '', '0-无超时；1-起升超时', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('152', '2', '53', 'HorizontalLaserDataError', '0', 'DB101.DBX38.6', '', '0-无错误；1-水平激光数据错误', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('153', '2', '54', 'RaisingBarcodeDataError', '0', 'DB101.DBX38.7', '', '0-无错误；1-起升条码数据错误', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('154', '2', '55', 'AdressError', '0', 'DB101.DBX39.0', '', '0-无故障；1-地址错', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('155', '2', '56', 'MainCocontactorInterrupt', '0', 'DB101.DBX39.1', '', '0-无故障；1-主接触器断开', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('156', '2', '57', 'HorizontalBreakerOrBrakeInterrupt', '0', 'DB101.DBX39.2', '', '0-无故障；1-水平断路器/制动器跳闸', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('157', '2', '58', 'RaisingBreakerOrBrakeInterrupt', '0', 'DB101.DBX39.3', '', '0-无故障；1-起升断路器/制动器跳闸', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('158', '2', '59', 'HorizontalLeadingendOut', '0', 'DB101.DBX39.4', '', '0-无超限，1-水平前端超限（前进终点）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('159', '2', '60', 'HorizontalTrailingendOut', '0', 'DB101.DBX39.5', '', '0-无超限；1-水平后端超限（后退终点）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('160', '2', '61', 'VerticalHorizontalLeadingendOut', '0', 'DB101.DBX39.6', '', '0-无超限；1-垂直上端超限（上升终点）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('161', '2', '62', 'VerticalHorizontalTrailingendOut', '0', 'DB101.DBX39.7', '', '0-无超限；1-垂直下端超限（下降终点）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('162', '2', '63', 'HorizontalUuivertorSpeed', '0', 'DB101.DBX40.0', '', '0-无过载；1-水平变频器速度超过设定值', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('163', '2', '64', 'RaisingUuivertorSpeed', '0', 'DB101.DBX40.1', '', '0-无松绳；1-起升变频器速度超过设定值', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('164', '2', '149', 'WCSForkAction', '0', 'DB100.DBW0', '', '0=无，1=1号货叉，2=2号货叉，3=同时动作', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('165', '2', '150', 'WCSFork1TaskFlag', '0', 'DB100.DBW2', '', '0-无任务,1-库内取货,2-库内放货,3-库外入库,4库外出库,5重新分配,6删除,10完成', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('166', '2', '151', 'WCSFork1Row', '0', 'DB100.DBW4', '', '取放货地址:  排  (1=左1，2=左2，3=右1，4=右2)', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('167', '2', '152', 'WCSFork1Column', '0', 'DB100.DBW8', '', '取放货地址: 列（1-最远列）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('168', '2', '153', 'WCSFork1Layer', '0', 'DB100.DBW10', '', '取放货地址: 层（1-最高层）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('169', '2', '154', 'WCSFork1Station', '0', 'DB100.DBW6', '', '取放货出入口（1-10）', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('170', '2', '155', 'WCSFork1Task', '0', 'DB100.DBD22', '', '任务号1', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('171', '2', '156', 'WCSHeartBeat', '0', 'DB100.DBW38', '', '心跳 累加 1-32767', '2018-11-14 14:54:02', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('382', '6', '276', 'RequestMessage', '0', 'DB3000.DBW120', '', '地址请求', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('383', '6', '277', 'RequestLoadStatus', '0', 'DB3000.DBW122', '', '装载状态', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('384', '6', '278', 'RequestNumber', '0', 'DB3000.DBW124', '', '请求编码 （地址编码）', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('385', '6', '279', 'RequestBarcode', '0', 'DB3000.DBC126.20', '', '请求条码', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('386', '6', '280', 'RequestWeight', '0', 'DB3000W146', '', '重量', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('387', '6', '281', 'RequestLength', '0', 'DB3000.DBW148', '', '长度', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('388', '6', '282', 'RequestWidth', '0', 'DB3000.DBW150', '', '宽度', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('389', '6', '283', 'RequestHeight', '0', 'DB3000.DBW152', '', '高度', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('390', '6', '284', 'RequestRetCode', '0', 'DB3000.DBW154', '', '返回代码', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('392', '6', '286', 'ArriveMessage', '0', 'DB3001.DBW100', '', '到达信息（分拣信息）', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('393', '6', '287', 'ArriveResult', '0', 'DB3001.DBW102', '', '到达结果 1成功，  2失败', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('394', '6', '288', 'ArriveRealAddress', '0', 'DB3001.DBW104', '', '达到地址  （地址编码）', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('395', '6', '289', 'ArriveAllcationAddress', '0', 'DB3001.DBW106', '', 'WCS分配地址', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('396', '6', '290', 'ArriveBarcode', '0', 'DB3001.DBC108.20', '', '条码', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('406', '6', '300', 'WCSReplyMessage', '0', 'DB3102.DBW120', '', '地址回复信息', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('407', '6', '301', 'WCSReplyLoadStatus', '0', 'DB3102.DBW122', '', '地址回复装载状态', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('408', '6', '302', 'WCSReplyNumber', '0', 'DB3102.DBW124', '', '地址编码', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('409', '6', '303', 'WCSReplyBarcode', '0', 'DB3102.DBC126.20', '', '条码', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('410', '6', '304', 'WCSReplyWeight', '0', 'DB3102.DBW146', '', '重量', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('411', '6', '305', 'WCSReplyLength', '0', 'DB3102.DBW148', '', '长度', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('412', '6', '306', 'WCSReplyWidth', '0', 'DB3102.DBW150', '', '宽度', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('413', '6', '307', 'WCSReplyHeight', '0', 'DB3102.DBW152', '', '高度', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('414', '6', '308', 'WCSReplyAddress', '0', 'DB3102.DBW154', '', '分配地址', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('420', '6', '314', 'WCSACKMessage', '0', 'DB3101.DBW30', '', 'ACK回复', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('421', '6', '315', 'WCSACKLoadStatus', '0', 'DB3101.DBW32', '', '', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('422', '6', '316', 'WCSACKNumber', '0', 'DB3101.DBW34', '', '', '2018-11-16 14:41:39', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('434', '3', '244', 'ArriveMessage', '0', 'DB3001.DBW40', '', '', '2018-11-16 15:37:54', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('435', '3', '245', 'ArriveResult', '0', 'DB3001.DBW42', '', '', '2018-11-16 15:37:54', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('436', '3', '246', 'ArriveRealAddress', '0', 'DB3001.DBW44', '', '', '2018-11-16 15:37:54', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('437', '3', '247', 'ArriveAllcationAddress', '0', 'DB3001.DBW46', '', '', '2018-11-16 15:37:54', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('438', '3', '248', 'ArriveBarcode', '0', 'DB3001.DBC48.20', '', '', '2018-11-16 15:37:54', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('462', '3', '272', 'WCSACKMessage', '0', 'DB3101.DBW10', '', '', '2018-11-16 15:37:54', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('463', '3', '273', 'WCSACKLoadStatus', '0', 'DB3101.DBW12', '', '', '2018-11-16 15:37:54', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('464', '3', '274', 'WCSACKNumber', '0', 'DB3101.DBW14', '', '', '2018-11-16 15:37:54', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('520', '8', '377', 'ArriveMessage', '0', 'DB3001.DBW0', '', '', '2018-11-26 09:46:37', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('521', '8', '378', 'ArriveResult', '0', 'DB3001.DBW2', '', '', '2018-11-26 09:46:37', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('522', '8', '379', 'ArriveRealAddress', '0', 'DB3001.DBW4', '', '', '2018-11-26 09:46:37', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('523', '8', '380', 'ArriveAllcationAddress', '0', 'DB3001.DBW6', '', '', '2018-11-26 09:46:37', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('524', '8', '381', 'ArriveBarcode', '0', 'DB3001.DBC8.20', '', '', '2018-11-26 09:46:37', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('548', '8', '405', 'WCSACKMessage', '0', 'DB3101.DBW0', '', '', '2018-11-26 09:46:37', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('549', '8', '406', 'WCSACKLoadStatus', '0', 'DB3101.DBW2', '', '', '2018-11-26 09:46:37', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('550', '8', '407', 'WCSACKNumber', '0', 'DB3101.DBW4', '', '', '2018-11-26 09:46:37', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('552', '9', '409', 'RequestMessage', '0', 'DB3000.DBW40', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('553', '9', '410', 'RequestLoadStatus', '0', 'DB3000.DBW42', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('554', '9', '411', 'RequestNumber', '0', 'DB3000.DBW44', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('555', '9', '412', 'RequestBarcode', '0', 'DB3000.DBC46.20', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('556', '9', '413', 'RequestWeight', '0', 'DB3000.DBW66', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('557', '9', '414', 'RequestLength', '0', 'DB3000.DBW68', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('558', '9', '415', 'RequestWidth', '0', 'DB3000.DBW70', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('559', '9', '416', 'RequestHeight', '0', 'DB3000.DBW72', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('560', '9', '417', 'RequestRetCode', '0', 'DB3000.DBW74', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('576', '9', '433', 'WCSReplyMessage', '0', 'DB3102.DBW40', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('577', '9', '434', 'WCSReplyLoadStatus', '0', 'DB3102.DBW42', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('578', '9', '435', 'WCSReplyNumber', '0', 'DB3102.DBW44', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('579', '9', '436', 'WCSReplyBarcode', '0', 'DB3102.DBC46.20', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('580', '9', '437', 'WCSReplyWeight', '0', 'DB3102.DBW66', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('581', '9', '438', 'WCSReplyLength', '0', 'DB3102.DBW68', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('582', '9', '439', 'WCSReplyWidth', '0', 'DB3102.DBW70', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('583', '9', '440', 'WCSReplyHeight', '0', 'DB3102.DBW72', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('584', '9', '441', 'WCSReplyAddress', '0', 'DB3102.DBW74', '', '', '2018-11-26 09:46:44', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('594', '10', '451', 'RequestMessage', '0', 'DB3000.DBW80', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('595', '10', '452', 'RequestLoadStatus', '0', 'DB3000.DBW82', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('596', '10', '453', 'RequestNumber', '0', 'DB3000.DBW84', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('597', '10', '454', 'RequestBarcode', '0', 'DB3000.DBC86.20', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('598', '10', '455', 'RequestWeight', '0', 'DB3000.DBW106', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('599', '10', '456', 'RequestLength', '0', 'DB3000.DBW108', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('600', '10', '457', 'RequestWidth', '0', 'DB3000.DBW110', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('601', '10', '458', 'RequestHeight', '0', 'DB3000.DBW112', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('602', '10', '459', 'RequestRetCode', '0', 'DB3000.DBW114', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('604', '10', '461', 'ArriveMessage', '0', 'DB3001.DBW70', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('605', '10', '462', 'ArriveResult', '0', 'DB3001.DBW72', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('606', '10', '463', 'ArriveRealAddress', '0', 'DB3001.DBW74', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('607', '10', '464', 'ArriveAllcationAddress', '0', 'DB3001.DBW76', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('608', '10', '465', 'ArriveBarcode', '0', 'DB3001.DBC78.20', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('618', '10', '475', 'WCSReplyMessage', '0', 'DB3102.DBW80', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('619', '10', '476', 'WCSReplyLoadStatus', '0', 'DB3102.DBW82', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('620', '10', '477', 'WCSReplyNumber', '0', 'DB3102.DBW84', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('621', '10', '478', 'WCSReplyBarcode', '0', 'DB3102.DBC86.20', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('622', '10', '479', 'WCSReplyWeight', '0', 'DB3102.DBW106', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('623', '10', '480', 'WCSReplyLength', '0', 'DB3102.DBW108', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('624', '10', '481', 'WCSReplyWidth', '0', 'DB3102.DBW110', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('625', '10', '482', 'WCSReplyHeight', '0', 'DB3102.DBW112', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('626', '10', '483', 'WCSReplyAddress', '0', 'DB3102.DBW114', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('632', '10', '489', 'WCSACKMessage', '0', 'DB3101.DBW20', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('633', '10', '490', 'WCSACKLoadStatus', '0', 'DB3101.DBW22', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('634', '10', '491', 'WCSACKNumber', '0', 'DB3101.DBW24', '', '', '2018-11-26 09:46:51', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('638', '2', '493', 'WCSCanOut', '0', 'DB3050.DBX0.0', '', '', '2018-11-26 13:30:40', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('639', '2', '494', 'SwitchStatus', '0', 'DB3050.DBX2.0', '', '', '2018-11-26 13:30:40', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('640', '7', '325', 'RequestMessage', '0', 'DB3000.DBW0', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('641', '7', '326', 'RequestLoadStatus', '0', 'DB3000.DBW2', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('642', '7', '327', 'RequestNumber', '0', 'DB3000.DBW4', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('643', '7', '328', 'RequestBarcode', '0', 'DB3000.DBC6.20', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('644', '7', '329', 'RequestWeight', '0', 'DB3000.DBW26', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('645', '7', '330', 'RequestLength', '0', 'DB3000.DBW28', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('646', '7', '331', 'RequestWidth', '0', 'DB3000.DBW30', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('647', '7', '332', 'RequestHeight', '0', 'DB3000.DBW32', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('648', '7', '333', 'RequestRetCode', '0', 'DB3000.DBW34', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('664', '7', '349', 'WCSReplyMessage', '0', 'DB3102.DBW0', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('665', '7', '350', 'WCSReplyLoadStatus', '0', 'DB3102.DBW2', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('666', '7', '351', 'WCSReplyNumber', '0', 'DB3102.DBW4', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('667', '7', '352', 'WCSReplyBarcode', '0', 'DB3102.DBC6.20', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('668', '7', '353', 'WCSReplyWeight', '0', 'DB3102.DBW26', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('669', '7', '354', 'WCSReplyLength', '0', 'DB3102.DBW28', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('670', '7', '355', 'WCSReplyWidth', '0', 'DB3102.DBW30', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('671', '7', '356', 'WCSReplyHeight', '0', 'DB3102.DBW32', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('672', '7', '357', 'WCSReplyAddress', '0', 'DB3102.DBW34', '', '', '2018-11-26 15:55:29', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('673', '3', '234', 'RequestMessage', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('674', '3', '235', 'RequestLoadStatus', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('675', '3', '236', 'RequestNumber', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('676', '3', '237', 'RequestBarcode', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('677', '3', '238', 'RequestWeight', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('678', '3', '239', 'RequestLength', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('679', '3', '240', 'RequestWidth', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('680', '3', '241', 'RequestHeight', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('681', '3', '242', 'RequestRetCode', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('682', '3', '243', 'RequestBackup', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('683', '3', '249', 'ArrivePaddingBit', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('684', '3', '250', 'ControlMessage', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('685', '3', '251', 'ControlType', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('686', '3', '252', 'ControlNumber', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('687', '3', '253', 'ControlBackUp', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('688', '3', '254', 'ACKMessage', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('689', '3', '255', 'ACKLoadStatus', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('690', '3', '256', 'ACKNumber', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('691', '3', '257', 'ACKBackUp', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('692', '3', '258', 'WCSReplyMessage', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('693', '3', '259', 'WCSReplyLoadStatus', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('694', '3', '260', 'WCSReplyNumber', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('695', '3', '261', 'WCSReplyBarcode', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('696', '3', '262', 'WCSReplyWeight', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('697', '3', '263', 'WCSReplyLength', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('698', '3', '264', 'WCSReplyWidth', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('699', '3', '265', 'WCSReplyHeight', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('700', '3', '266', 'WCSReplyAddress', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('701', '3', '267', 'WCSReplyBackUp', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('702', '3', '268', 'WCSControlMessage', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('703', '3', '269', 'WCSControlType', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('704', '3', '270', 'WCSControlNumber', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('705', '3', '271', 'WCSControlBackup', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('706', '3', '275', 'WCSACKBackup', '0', '', '', '', '2019-09-02 16:40:00', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('833', '11', '234', 'RequestMessage', '0', 'DB3001W', '', '地址请求', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('834', '11', '235', 'RequestLoadStatus', '0', 'DB3001W', '', '地址请求-装载状态', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('835', '11', '236', 'RequestNumber', '0', 'DB3001W', '', '地址请求-读码器编号', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('836', '11', '237', 'RequestBarcode', '0', 'DB3001CHAR,20', '', '地址请求-条码', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('837', '11', '238', 'RequestWeight', '0', 'DB3001W', '', '地址请求-货物重量', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('838', '11', '239', 'RequestLength', '0', 'DB3001W', '', '地址请求-货物长度', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('839', '11', '240', 'RequestWidth', '0', 'DB3001W', '', '地址请求-货物宽度', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('840', '11', '241', 'RequestHeight', '0', 'DB3001W', '', '地址请求-货物高度', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('841', '11', '242', 'RequestRetCode', '0', 'DB3001CHAR,20', '', '地址请求-RetCode', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('842', '11', '243', 'RequestBackup', '0', 'DB3001CHAR,20', '', '地址请求-备用', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('843', '11', '244', 'ArriveMessage', '0', 'DB3001W', '', '位置到达-报文', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('844', '11', '245', 'ArriveResult', '0', 'DB3001W', '', '位置到达-结果', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('845', '11', '246', 'ArriveRealAddress', '0', 'DB3001W', '', '位置到达-实际到达地址', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('846', '11', '247', 'ArriveAllcationAddress', '0', 'DB3001W', '', '位置到达-WCS分配地址', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('847', '11', '248', 'ArriveBarcode', '0', 'DB3001CHAR,20', '', '位置到达-条码', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('848', '11', '249', 'ArrivePaddingBit', '0', 'DB3001W', '', '位置到达-填充位', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('849', '11', '250', 'ControlMessage', '0', 'DB3001W', '', '控制指令-报文', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('850', '11', '251', 'ControlType', '0', 'DB3001W', '', '控制指令-类型', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('851', '11', '252', 'ControlNumber', '0', 'DB3001W', '', '控制指令-站台编号', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('852', '11', '253', 'ControlBackUp', '0', 'DB3001CHAR,20', '', '控制指令-备用', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('853', '11', '254', 'ACKMessage', '0', 'DB3001W', '', 'ACK报文', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('854', '11', '255', 'ACKLoadStatus', '0', 'DB3001W', '', 'ACK装载状态', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('855', '11', '256', 'ACKNumber', '0', 'DB3001W', '', 'ACK读码器编号', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('856', '11', '257', 'ACKBackUp', '0', 'DB3001CHAR,20', '', 'ACK备用', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('857', '11', '258', 'WCSReplyMessage', '0', 'DB3001W', '', 'WCS地址回复报文', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('858', '11', '259', 'WCSReplyLoadStatus', '0', 'DB3001W', '', 'WCS地址回复-装载状态', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('859', '11', '260', 'WCSReplyNumber', '0', 'DB3001W', '', 'WCS地址回复-读码器编码', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('860', '11', '261', 'WCSReplyBarcode', '0', 'DB3001CHAR,20', '', 'WCS地址回复-条码', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('861', '11', '262', 'WCSReplyWeight', '0', 'DB3001W', '', 'WCS地址回复-货物重量', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('862', '11', '263', 'WCSReplyLength', '0', 'DB3001W', '', 'WCS地址回复-货物长度', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('863', '11', '264', 'WCSReplyWidth', '0', 'DB3001W', '', 'WCS地址回复-货物宽度', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('864', '11', '265', 'WCSReplyHeight', '0', 'DB3001W', '', 'WCS地址回复-货物高度', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('865', '11', '266', 'WCSReplyAddress', '0', 'DB3001W', '', 'WCS地址回复-目标地址', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('866', '11', '267', 'WCSReplyBackUp', '0', 'DB3001CHAR,20', '', 'WCS地址回复-备用', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('867', '11', '268', 'WCSControlMessage', '0', 'DB3001W', '', 'WCS控制指令-报文', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('868', '11', '269', 'WCSControlType', '0', 'DB3001W', '', 'WCS控制指令-报文类型', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('869', '11', '270', 'WCSControlNumber', '0', 'DB3001W', '', 'WCS控制指令-读码器编号', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('870', '11', '271', 'WCSControlBackup', '0', 'DB3001CHAR,20', '', 'WCS控制指令-备用', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('871', '11', '272', 'WCSACKMessage', '0', 'DB3001W', '', 'WCSACK报文', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('872', '11', '273', 'WCSACKLoadStatus', '0', 'DB3001W', '', 'WCSACK-装载状态', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('873', '11', '274', 'WCSACKNumber', '0', 'DB3001W', '', 'WCSACK-读码器编码', '2019-10-15 17:41:16', '', null, null);
INSERT INTO `equipment_prop2` VALUES ('874', '11', '275', 'WCSACKBackup', '0', 'DB3001CHAR,20', '', 'WCSACK-备用', '2019-10-15 17:41:16', '', null, null);

-- ----------------------------
-- Table structure for equipment_type
-- ----------------------------
DROP TABLE IF EXISTS `equipment_type`;
CREATE TABLE `equipment_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) COLLATE utf8_bin NOT NULL,
  `name` varchar(50) COLLATE utf8_bin NOT NULL,
  `description` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_bin ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of equipment_type
-- ----------------------------
INSERT INTO `equipment_type` VALUES ('2', 'DoubleForkStacker', '双叉堆垛机', null, '2018-11-14 10:08:48', 'admin', null, null);
INSERT INTO `equipment_type` VALUES ('3', 'stocker', '单叉堆垛机', null, '2018-11-14 10:09:36', 'admin', null, null);
INSERT INTO `equipment_type` VALUES ('4', 'Station', '标准站台', null, '2018-11-14 13:36:14', 'admin', null, null);
INSERT INTO `equipment_type` VALUES ('5', 'StationForPortOut', '出库口', '只出库操作口', '2018-11-16 14:11:50', 'admin', null, null);
INSERT INTO `equipment_type` VALUES ('7', 'StationForPortInOrOut', '出\\入口', '出入站台', '2018-11-16 14:17:34', 'admin', null, null);
INSERT INTO `equipment_type` VALUES ('8', 'StationForPortIn', '入库口', '只入库操作', '2018-11-23 15:38:06', 'liufu', null, null);
INSERT INTO `equipment_type` VALUES ('9', 'StationForStockerIn', '接入站台', null, '2018-11-23 16:27:05', 'liufu', null, null);
INSERT INTO `equipment_type` VALUES ('10', 'StationForStockerOut', '接出站台', null, '2018-11-23 16:28:49', 'liufu', null, null);
INSERT INTO `equipment_type` VALUES ('11', 'StationForStockerInOrOut', '接出\\入站台', null, '2018-11-26 09:13:54', 'liufu', null, null);
INSERT INTO `equipment_type` VALUES ('12', 'SuperStocker', '高速堆垛机', '高速', '2019-07-09 18:53:58', 'admin', null, null);

-- ----------------------------
-- Table structure for wcsequipmenttype2
-- ----------------------------
DROP TABLE IF EXISTS `wcsequipmenttype2`;
CREATE TABLE `wcsequipmenttype2` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) COLLATE utf8_bin NOT NULL,
  `name` varchar(50) COLLATE utf8_bin NOT NULL,
  `description` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsequipmenttype2
-- ----------------------------
INSERT INTO `wcsequipmenttype2` VALUES ('2', 'DoubleForkSRM', '双叉堆垛机', null, '2018-11-14 10:08:48', 'admin', '2019-09-04 11:29:09', 'admin');
INSERT INTO `wcsequipmenttype2` VALUES ('3', 'SingeForkSRM', '单叉堆垛机', null, '2018-11-14 10:09:36', 'admin', '2019-09-04 11:29:19', 'admin');
INSERT INTO `wcsequipmenttype2` VALUES ('4', 'Station', '标准站台', null, '2018-11-14 13:36:14', 'admin', null, null);
INSERT INTO `wcsequipmenttype2` VALUES ('5', 'StationForPortOut', '只出站台', null, '2018-11-16 14:11:50', 'admin', null, null);
INSERT INTO `wcsequipmenttype2` VALUES ('7', 'StationForPortInOrOut', '出\\入站台', '湘电可由开关控制的出入站台', '2018-11-16 14:17:34', 'admin', null, null);
INSERT INTO `wcsequipmenttype2` VALUES ('8', 'StationForPortIn', '只入站台', null, '2018-11-23 15:38:06', 'liufu', null, null);
INSERT INTO `wcsequipmenttype2` VALUES ('9', 'StationForStockerIn', '标准堆垛机接入站台', null, '2018-11-23 16:27:05', 'liufu', null, null);
INSERT INTO `wcsequipmenttype2` VALUES ('10', 'StationForStockerOut', '标准堆垛机接出站台', null, '2018-11-23 16:28:49', 'liufu', null, null);
INSERT INTO `wcsequipmenttype2` VALUES ('11', 'StationForStockerInOrOut', '开关控制堆垛机接入接出站台', null, '2018-11-26 09:13:54', 'liufu', null, null);
INSERT INTO `wcsequipmenttype2` VALUES ('13', 'StationForStockerInOrOut', '开关控制堆垛机接入接出站台', null, '2018-11-26 09:13:54', 'liufu', null, null);
INSERT INTO `wcsequipmenttype2` VALUES ('14', 'SuperSRM', '高速堆垛机', '高速', '2019-09-04 11:54:35', 'admin', null, null);
INSERT INTO `wcsequipmenttype2` VALUES ('15', 'StationStatusMonitor', '站台可出状态监控', '站台可出状态监控', '2019-09-04 12:02:41', 'admin', null, null);

-- ----------------------------
-- Table structure for wcsequipmenttypeproptemplate
-- ----------------------------
DROP TABLE IF EXISTS `wcsequipmenttypeproptemplate`;
CREATE TABLE `wcsequipmenttypeproptemplate` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) COLLATE utf8_bin NOT NULL,
  `name` varchar(50) COLLATE utf8_bin NOT NULL,
  `equipmentTypeId` int(11) NOT NULL,
  `description` varchar(200) COLLATE utf8_bin DEFAULT NULL,
  `propType` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '指定该属性的类型，比如设备固有属性、PLC地址属性等',
  `dataType` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '属性的数据类型，用作给PLC传值的时候转换数据类型',
  `address` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `offset` varchar(10) COLLATE utf8_bin DEFAULT NULL COMMENT '地址偏移量',
  `readLength` int(11) DEFAULT '1' COMMENT '读取长度(暂未使用)',
  `isMonitor` tinyint(4) NOT NULL,
  `monitorCompareValue` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `monitorNormal` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `monitorFailure` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=573 DEFAULT CHARSET=utf8 COLLATE=utf8_bin ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of wcsequipmenttypeproptemplate
-- ----------------------------
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('9', 'Number', '堆垛机编号', '3', null, 'PLC', 'INT', 'DB101W0', '0', '1', '0', null, null, null, '2018-11-14 10:11:39', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('10', 'OperationModel', '操作模式', '3', '操作模式：1-维修；2-手动；3-机载操作；4-单机自动；5-联机', 'PLC', 'INT', 'DB101W2', '2', '1', '0', null, null, null, '2018-11-14 10:12:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('11', 'HeartBeat', '心跳', '3', null, 'PLC', 'INT', 'DB101W4', '4', '1', '0', null, null, null, '2018-11-14 10:13:24', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('12', 'TaskLimit', '任务限制', '3', '任务限制：1-无限制，2-入库限制，3-出库限制', 'PLC', 'INT', 'DB101W6', '6', '1', '0', null, null, null, '2018-11-14 10:14:12', 'admin', '2018-11-14 10:16:13', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('13', 'Fork1TaskExcuteStatus', '货叉1_任务执行', '3', '1-待机；2-任务执行中;3-任务完成；4-任务中断（出错）；5-下发任务错误', 'PLC', 'INT', 'DB101W42', '42', '1', '0', null, null, null, '2018-11-14 10:20:26', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('14', 'Fork1TaskNo', '货叉1_任务号', '3', '任务号', 'PLC', 'DINT', 'DB101D46', '46', '1', '0', null, null, null, '2018-11-14 10:21:18', 'admin', '2018-11-23 14:57:03', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('15', 'Fork1TaskType', '货叉1_任务类型', '3', '货叉任务类型：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库', 'PLC', 'INT', 'DB101W54', '54', '1', '0', null, null, null, '2018-11-14 10:23:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('16', 'HorizontalDistance', '水平测距', '3', '水平测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D8', '8', '1', '0', null, null, null, '2018-11-14 10:27:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('17', 'VerticalDistance', '起升测距', '3', '起升测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D12', '12', '1', '0', null, null, null, '2018-11-14 10:28:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('18', 'For1kDistance', '货叉1伸叉测距', '3', '货叉1伸叉测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D16', '16', '1', '0', null, null, null, '2018-11-14 10:30:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('19', 'CurrentColumn', '当前列', '3', null, 'PLC', 'INT', 'DB101W24', '24', '1', '0', null, null, null, '2018-11-14 10:30:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('20', 'CurrentLayer', '当前层', '3', null, 'PLC', 'INT', 'DB101W26', '26', '1', '0', null, null, null, '2018-11-14 10:31:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('21', 'CurrentStation', '当前出/入口', '3', '当前出/入口 1-10', 'PLC', 'INT', 'DB101W28', '28', '1', '0', null, null, null, '2018-11-14 10:32:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('22', 'Fork1FrontOut', '货叉1_是否货物前超', '3', '-无超限；1-货物前超', 'PLC', 'BOOL', 'DB101X30.0', '30.0', '1', '1', 'False', '无超限', '货物前超', '2018-11-14 10:34:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('23', 'Fork1BehindOut', '货叉1_是否货物后超', '3', null, 'PLC', 'BOOL', 'DB101X30.1', '30.1', '1', '1', 'False', '无超限', '货物后超', '2018-11-14 10:35:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('24', 'Fork1LeftForkOut', '货叉1_是否左侧外形超限', '3', null, 'PLC', 'BOOL', 'DB101X30.2', '30.2', '1', '1', 'False', '无超限', '左侧外形超限', '2018-11-14 10:37:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('25', 'Fork1RightForkOut', '货叉1_是否右侧外形超限', '3', null, 'PLC', 'BOOL', 'DB101X30.3', '30.3', '1', '1', 'False', '无超限', '右侧外形超限', '2018-11-14 10:38:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('26', 'Fork1OverHeight1', '货叉1_超高1', '3', null, 'PLC', 'BOOL', 'DB101X30.4', '30.4', '1', '1', 'False', '无超高', '超高 1（货物高度与送货地址不匹配）', '2018-11-14 10:39:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('27', 'Fork1OverHeight2', '货叉1_超高2', '3', null, 'PLC', 'BOOL', 'DB101X30.5', '30.5', '1', '1', 'False', '无超高', '超高 2（货物高度与送货地址不匹配）', '2018-11-14 10:39:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('28', 'Fork1OverHeight3', '货叉1_超高3', '3', null, 'PLC', 'BOOL', 'DB101X30.6', '30.6', '1', '1', 'False', '无超高', '超高 3（货物高度与送货地址不匹配）', '2018-11-14 10:40:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('29', 'Fork1OverHeight', '货叉1_货物超高', '3', null, 'PLC', 'BOOL', 'DB101X30.7', '30.7', '1', '1', 'False', '无超高', '货物超高', '2018-11-14 10:40:50', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('30', 'Fork1PalletForkTimeout', '货叉1_货叉超时', '3', null, 'PLC', 'BOOL', 'DB101X31.0', '31.0', '1', '1', 'False', '无超时', '货叉超时', '2018-11-14 10:41:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('31', 'Fork1LeftLimitAlarm', '货叉1_是否左侧极限报警', '3', null, 'PLC', 'BOOL', 'DB101X31.1', '31.1', '1', '1', 'False', '无故障', '左侧极限报警', '2018-11-14 10:45:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('32', 'Fork1RightLimitAlarm', '货叉1_是否右侧极限报警', '3', null, 'PLC', 'BOOL', 'DB101X31.2', '31.2', '1', '1', 'False', '无故障', '右侧极限报警', '2018-11-14 10:47:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('33', 'Fork1ForkUuivertor', '货叉1_货叉变频器故障', '3', null, 'PLC', 'BOOL', 'DB101X31.3', '31.3', '1', '1', 'False', '无故障', '货叉变频器故障', '2018-11-14 10:48:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('34', 'Fork1ForkBreakerOrCocontactor', '货叉1_货叉断路器/接触器故障', '3', null, 'PLC', 'BOOL', 'DB101X31.4', '31.4', '1', '1', 'False', '无故障', '货叉断路器/接触器故障', '2018-11-14 10:49:14', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('35', 'Fork1GoodsInspectionSensor', '货叉1_是否货物检测传感器故障', '3', null, 'PLC', 'BOOL', 'DB101X31.5', '31.5', '1', '1', 'False', '无故障', '货物检测传感器故障', '2018-11-14 10:51:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('36', 'Fork1ForkAlignmentSensor', '货叉1_是否货叉定位传感器故障', '3', null, 'PLC', 'BOOL', 'DB101X31.6', '31.6', '1', '1', 'False', '无故障', '货叉定位传感器故障', '2018-11-14 10:52:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('37', 'Fork1DirectionError', '货叉1_是否运行方向错误', '3', null, 'PLC', 'BOOL', 'DB101X31.7', '31.7', '1', '1', 'False', '无故障', '运行方向错误', '2018-11-14 10:56:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('38', 'Fork1XYForkExcute', '货叉1_是否货叉执行动作错误', '3', null, 'PLC', 'BOOL', 'DB101X32.0', '32.0', '1', '1', 'False', '无故障', 'X轴、Y轴、货叉执行动作错误', '2018-11-14 10:58:48', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('39', 'Fork1SetValueError', '货叉1_是否设定值错误', '3', null, 'PLC', 'BOOL', 'DB101X32.1', '32.2', '1', '1', 'False', '无故障', '设定值错误', '2018-11-14 10:59:12', 'admin', '2018-11-22 10:02:58', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('40', 'Fork1PickupTaskError', '货叉1_是否取货任务错误', '3', '0-无故障；1-取货任务错误（取左2右2时，左1右1有货）', 'PLC', 'BOOL', 'DB101X32.2', '32.2', '1', '1', 'False', '无故障', '取货任务错误', '2018-11-14 10:59:28', 'admin', '2018-11-22 10:03:58', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('41', 'Fork1Spare3', '货叉1_Spare3', '3', null, 'PLC', 'BOOL', 'DB101X32.3', '32.3', '1', '0', null, null, null, '2018-11-14 10:59:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('42', 'Fork1DoubleIn', '货叉1_双重入库', '3', null, 'PLC', 'BOOL', 'DB101X32.4', '32.4', '1', '1', 'False', '无故障', '双重入库 满入', '2018-11-14 11:00:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('43', 'Fork1EmptyOut', '货叉1_是否空货位出库', '3', null, 'PLC', 'BOOL', 'DB101X32.5', '32.5', '1', '1', 'False', '无故障', '空货位出库 空出', '2018-11-14 11:01:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('44', 'Fork1ForkHasPallet', '货叉1_是否货叉有货', '3', null, 'PLC', 'BOOL', 'DB101X32.6', '32.6', '1', '0', 'False', '货叉无货', '货叉有货', '2018-11-14 11:03:10', 'admin', '2018-11-19 19:22:32', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('45', 'Fork1ForkError', '货叉1_货叉总故障', '3', null, 'PLC', 'BOOL', 'DB101X32.7', '32.7', '1', '1', 'False', '无故障', '货叉故障', '2018-11-14 11:03:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('46', 'Fork1Spare4', '货叉1_Spare4', '3', null, 'PLC', 'BYTE', 'DB101B33', '33', '1', '0', null, null, null, '2018-11-14 11:04:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('47', 'Overload', '是否过载', '3', null, 'PLC', 'BOOL', 'DB101X38.0', '38.0', '1', '1', 'False', '无过载', '堆垛机故障', '2018-11-14 11:07:32', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('48', 'Rope', '是否松绳', '3', null, 'PLC', 'BOOL', 'DB101X38.1', '38.1', '1', '1', 'False', '无松绳', '松绳', '2018-11-14 11:08:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('49', 'RunningUuivertorAlarm', '是否行走变频器报警', '3', null, 'PLC', 'BOOL', 'DB101X38.2', '38.2', '1', '1', 'False', '无报警', '行走变频器报警', '2018-11-14 11:08:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('50', 'RaisingUuivertorAlarm', '是否起升变频器报警', '3', null, 'PLC', 'BOOL', 'DB101X38.3', '38.3', '1', '1', 'False', '无报警', '起升变频器报警', '2018-11-14 11:09:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('51', 'RunningTimeout', '是否运行超时', '3', null, 'PLC', 'BOOL', 'DB101X38.4', '38.4', '1', '1', 'False', '无超时', '运行超时', '2018-11-14 11:10:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('52', 'RaisingTimeout', '是否起升超时', '3', null, 'PLC', 'BOOL', 'DB101X38.5', '38.5', '1', '1', 'False', '无超时', '起升超时', '2018-11-14 11:10:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('53', 'HorizontalLaserDataError', '是否水平激光数据错误', '3', null, 'PLC', 'BOOL', 'DB101X38.6', '38.6', '1', '1', 'False', '无错误', '水平激光数据错误', '2018-11-14 11:11:23', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('54', 'RaisingBarcodeDataError', '起升条码数据错误', '3', null, 'PLC', 'BOOL', 'DB101X38.7', '38.7', '1', '1', 'False', '无错误', '起升条码数据错误', '2018-11-14 11:11:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('55', 'AdressError', '是否地址错', '3', null, 'PLC', 'BOOL', 'DB101X39.0', '39.0', '1', '1', 'False', '无故障', '地址错', '2018-11-14 11:12:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('56', 'MainCocontactorInterrupt', '主接触器断开', '3', '急停、冲 顶、超速保 护，行走超限动作', 'PLC', 'BOOL', 'DB101X39.1', '39.1', '1', '1', 'False', '无故障', '主接触器断开', '2018-11-14 11:14:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('57', 'HorizontalBreakerOrBrakeInterrupt', '水平断路器/制动器跳闸', '3', null, 'PLC', 'BOOL', 'DB101X39.2', '39.2', '1', '1', 'False', '无故障', '水平断路器/制动器跳闸', '2018-11-14 11:15:09', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('58', 'RaisingBreakerOrBrakeInterrupt', '是否起升断路器/制动器跳闸', '3', null, 'PLC', 'BOOL', 'DB101X39.3', '39.3', '1', '1', 'False', '无故障', '起升断路器/制动器跳闸', '2018-11-14 11:15:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('59', 'HorizontalLeadingendOut', '是否水平前端超限（前进终点）', '3', null, 'PLC', 'BOOL', 'DB101X39.4', '39.4', '1', '1', 'False', '无超限', '水平前端超限（前进终点）', '2018-11-14 11:16:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('60', 'HorizontalTrailingendOut', '是否水平后端超限（后退终点）', '3', null, 'PLC', 'BOOL', 'DB101X39.5', '39.5', '1', '1', 'False', '无超限', '水平后端超限（后退终点）', '2018-11-14 11:17:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('61', 'VerticalHorizontalLeadingendOut', '是否垂直上端超限（上升终点）', '3', null, 'PLC', 'BOOL', 'DB101X39.6', '39.6', '1', '1', 'False', '无超限', '垂直上端超限（上升终点）', '2018-11-14 11:18:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('62', 'VerticalHorizontalTrailingendOut', '垂直下端超限（下降终点）', '3', null, 'PLC', 'BOOL', 'DB101X39.7', '39.7', '1', '1', 'False', '无超限', '垂直下端超限（下降终点）', '2018-11-14 11:19:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('63', 'HorizontalUuivertorSpeed', '是否水平变频器速度超过设定值', '3', null, 'PLC', 'BOOL', 'DB101X40.0', '40.0', '1', '1', 'False', '无过载', '水平变频器速度超过设定值', '2018-11-14 11:21:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('64', 'RaisingUuivertorSpeed', '是否起升变频器速度超过设定值', '3', null, 'PLC', 'BOOL', 'DB101X40.1', '40.1', '1', '1', 'False', '无松绳', '起升变频器速度超过设定值', '2018-11-14 11:23:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('65', 'Number', '堆垛机编号', '2', null, 'PLC', 'INT', 'DB101W0', '0', '1', '0', null, null, null, '2018-11-14 10:11:39', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('66', 'OperationModel', '操作模式', '2', '操作模式：1-维修；2-手动；3-机载操作；4-单机自动；5-联机', 'PLC', 'INT', 'DB101W2', '2', '1', '0', null, null, null, '2018-11-14 10:12:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('67', 'HeartBeat', '心跳', '2', null, 'PLC', 'INT', 'DB101W4', '4', '1', '0', null, null, null, '2018-11-14 10:13:24', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('68', 'TaskLimit', '任务限制', '2', '任务限制：1-无限制，2-入库限制，3-出库限制', 'PLC', 'INT', 'DB101W6', '6', '1', '0', null, null, null, '2018-11-14 10:14:12', 'admin', '2018-11-14 10:16:13', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('69', 'Fork1TaskExcuteStatus', '货叉1_任务执行', '2', '1-待机；2-任务执行中;3-任务完成；4-任务中断（出错）；5-下发任务错误', 'PLC', 'INT', 'DB101W42', '42', '1', '0', null, null, null, '2018-11-14 10:20:26', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('70', 'Fork1TaskNo', '货叉1_任务号', '2', '任务号', 'PLC', 'DINT', 'DB101D46', '46', '1', '0', null, null, null, '2018-11-14 10:21:18', 'admin', '2018-11-23 15:06:22', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('71', 'Fork1TaskType', '货叉1_任务类型', '2', '货叉任务类型：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库', 'PLC', 'INT', 'DB101W54', '54', '1', '0', null, null, null, '2018-11-14 10:23:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('72', 'Fork2TaskExcuteStatus', '货叉2_任务执行', '2', '1-待机；2-任务执行中;3-任务完成；4-任务中断（出错）；5-下发任务错误', 'PLC', 'INT', 'DB101W44', '44', '1', '0', null, null, null, '2018-11-14 10:20:26', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('73', 'Fork2TaskNo', '货叉2_任务号', '2', '任务号', 'PLC', 'DINT', 'DB101D50', '50', '1', '0', null, null, null, '2018-11-14 10:21:18', 'admin', '2018-11-23 15:06:29', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('74', 'Fork2TaskType', '货叉2_任务类型', '2', '货叉任务类型：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库', 'PLC', 'INT', 'DB101W56', '56', '1', '0', null, null, null, '2018-11-14 10:23:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('75', 'HorizontalDistance', '水平测距', '2', '水平测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D8', '8', '1', '0', null, null, null, '2018-11-14 10:27:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('76', 'VerticalDistance', '起升测距', '2', '起升测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D12', '12', '1', '0', null, null, null, '2018-11-14 10:28:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('77', 'Fork1Distance', '货叉1伸叉测距', '2', '货叉1伸叉测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D16', '16', '1', '0', null, null, null, '2018-11-14 10:30:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('78', 'CurrentColumn', '当前列', '2', null, 'PLC', 'INT', 'DB101W24', '24', '1', '0', null, null, null, '2018-11-14 10:30:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('79', 'CurrentLayer', '当前层', '2', null, 'PLC', 'INT', 'DB101W26', '26', '1', '0', null, null, null, '2018-11-14 10:31:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('80', 'CurrentStation', '当前出/入口', '2', '当前出/入口 1-10', 'PLC', 'INT', 'DB101W28', '28', '1', '0', null, null, null, '2018-11-14 10:32:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('81', 'Fork1FrontOut', '货叉1_是否货物前超', '2', '-无超限；1-货物前超', 'PLC', 'BOOL', 'DB101X30.0', '30.0', '1', '1', 'False', '无超限', '货物前超', '2018-11-14 10:34:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('82', 'Fork1BehindOut', '货叉1_是否货物后超', '2', null, 'PLC', 'BOOL', 'DB101X30.1', '30.1', '1', '1', 'False', '无超限', '货物后超', '2018-11-14 10:35:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('83', 'Fork1LeftForkOut', '货叉1_是否左侧外形超限', '2', null, 'PLC', 'BOOL', 'DB101X30.2', '30.2', '1', '1', 'False', '无超限', '左侧外形超限', '2018-11-14 10:37:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('84', 'Fork1RightForkOut', '货叉1_是否右侧外形超限', '2', null, 'PLC', 'BOOL', 'DB101X30.3', '30.3', '1', '1', 'False', '无超限', '右侧外形超限', '2018-11-14 10:38:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('85', 'Fork1OverHeight1', '货叉1_超高1', '2', null, 'PLC', 'BOOL', 'DB101X30.4', '30.4', '1', '1', 'False', '无超高', '超高 1（货物高度与送货地址不匹配）', '2018-11-14 10:39:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('86', 'Fork1OverHeight2', '货叉1_超高2', '2', null, 'PLC', 'BOOL', 'DB101X30.5', '30.5', '1', '1', 'False', '无超高', '超高 2（货物高度与送货地址不匹配）', '2018-11-14 10:39:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('87', 'Fork1OverHeight3', '货叉1_超高3', '2', null, 'PLC', 'BOOL', 'DB101X30.6', '30.6', '1', '1', 'False', '无超高', '超高 3（货物高度与送货地址不匹配）', '2018-11-14 10:40:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('88', 'Fork1OverHeight', '货叉1_货物超高', '2', null, 'PLC', 'BOOL', 'DB101X30.7', '30.7', '1', '1', 'False', '无超高', '货物超高', '2018-11-14 10:40:50', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('89', 'Fork1PalletForkTimeout', '货叉1_货叉超时', '2', null, 'PLC', 'BOOL', 'DB101X31.0', '31.0', '1', '1', 'False', '无超时', '货叉超时', '2018-11-14 10:41:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('90', 'Fork1LeftLimitAlarm', '货叉1_是否左侧极限报警', '2', null, 'PLC', 'BOOL', 'DB101X31.1', '31.1', '1', '1', 'False', '无故障', '左侧极限报警', '2018-11-14 10:45:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('91', 'Fork1RightLimitAlarm', '货叉1_是否右侧极限报警', '2', null, 'PLC', 'BOOL', 'DB101X31.2', '31.2', '1', '1', 'False', '无故障', '右侧极限报警', '2018-11-14 10:47:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('92', 'Fork1ForkUuivertor', '货叉1_货叉变频器故障', '2', null, 'PLC', 'BOOL', 'DB101X31.3', '31.3', '1', '1', 'False', '无故障', '货叉变频器故障', '2018-11-14 10:48:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('93', 'Fork1ForkBreakerOrCocontactor', '货叉1_货叉断路器/接触器故障', '2', null, 'PLC', 'BOOL', 'DB101X31.4', '31.4', '1', '1', 'False', '无故障', '货叉断路器/接触器故障', '2018-11-14 10:49:14', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('94', 'Fork1GoodsInspectionSensor', '货叉1_是否货物检测传感器故障', '2', null, 'PLC', 'BOOL', 'DB101X31.5', '31.5', '1', '1', 'False', '无故障', '货物检测传感器故障', '2018-11-14 10:51:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('95', 'Fork1ForkAlignmentSensor', '货叉1_是否货叉定位传感器故障', '2', null, 'PLC', 'BOOL', 'DB101X31.6', '31.6', '1', '1', 'False', '无故障', '货叉定位传感器故障', '2018-11-14 10:52:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('96', 'Fork1DirectionError', '货叉1_是否运行方向错误', '2', null, 'PLC', 'BOOL', 'DB101X31.7', '31.7', '1', '1', 'False', '无故障', '运行方向错误', '2018-11-14 10:56:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('97', 'Fork1XYForkExcute', '货叉1_是否货叉执行动作错误', '2', null, 'PLC', 'BOOL', 'DB101X32.0', '32.0', '1', '1', 'False', '无故障', 'X轴、Y轴、货叉执行动作错误', '2018-11-14 10:58:48', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('98', 'Fork1SetValueError', '货叉1_是否设定值错误', '2', null, 'PLC', 'BOOL', 'DB101X32.1', '32.2', '1', '1', 'False', '无故障', '设定值错误', '2018-11-14 10:59:12', 'admin', '2018-11-22 10:06:17', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('99', 'Fork1PickupTaskError', '货叉1_是否取货任务错误', '2', '0-无故障；1-取货任务错误（取左2右2时，左1右1有货）', 'PLC', 'BOOL', 'DB101X32.2', '32.2', '1', '1', 'False', '无故障', '取货任务错误', '2018-11-14 10:59:28', 'admin', '2018-11-22 10:06:41', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('100', 'Fork1Spare3', '货叉1_Spare3', '2', null, 'PLC', 'BOOL', 'DB101X32.3', '32.3', '1', '0', null, null, null, '2018-11-14 10:59:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('101', 'Fork1DoubleIn', '货叉1_双重入库', '2', null, 'PLC', 'BOOL', 'DB101X32.4', '32.4', '1', '1', 'False', '无故障', '双重入库 满入', '2018-11-14 11:00:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('102', 'Fork1EmptyOut', '货叉1_是否空货位出库', '2', null, 'PLC', 'BOOL', 'DB101X32.5', '32.5', '1', '1', 'False', '无故障', '空货位出库 空出', '2018-11-14 11:01:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('103', 'Fork1ForkHasPallet', '货叉1_是否货叉有货', '2', null, 'PLC', 'BOOL', 'DB101X32.6', '32.6', '1', '1', 'False', '货叉无货', '货叉有货', '2018-11-14 11:03:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('104', 'Fork1ForkError', '货叉1_货叉总故障', '2', null, 'PLC', 'BOOL', 'DB101X32.7', '32.7', '1', '1', 'False', '无故障', '货叉故障', '2018-11-14 11:03:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('105', 'Fork1Spare4', '货叉1_Spare4', '2', null, 'PLC', 'BYTE', 'DB101B33', '33', '1', '0', null, null, null, '2018-11-14 11:04:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('106', 'Fork2FrontOut', '货叉2_是否货物前超', '2', '-无超限；1-货物前超', 'PLC', 'BOOL', 'DB101X34.0', '34.0', '1', '1', 'False', '无超限', '货物前超', '2018-11-14 10:34:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('107', 'Fork2BehindOut', '货叉2_是否货物后超', '2', null, 'PLC', 'BOOL', 'DB101X34.1', '34.1', '1', '1', 'False', '无超限', '货物后超', '2018-11-14 10:35:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('108', 'Fork2LeftForkOut', '货叉2_是否左侧外形超限', '2', null, 'PLC', 'BOOL', 'DB101X34.2', '34.2', '1', '1', 'False', '无超限', '左侧外形超限', '2018-11-14 10:37:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('109', 'Fork2RightForkOut', '货叉2_是否右侧外形超限', '2', null, 'PLC', 'BOOL', 'DB101X34.3', '34.3', '1', '1', 'False', '无超限', '右侧外形超限', '2018-11-14 10:38:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('110', 'Fork2OverHeight1', '货叉2_超高1', '2', null, 'PLC', 'BOOL', 'DB101X34.4', '34.4', '1', '1', 'False', '无超高', '超高 1（货物高度与送货地址不匹配）', '2018-11-14 10:39:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('111', 'Fork2OverHeight2', '货叉2_超高2', '2', null, 'PLC', 'BOOL', 'DB101X34.5', '34.5', '1', '1', 'False', '无超高', '超高 2（货物高度与送货地址不匹配）', '2018-11-14 10:39:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('112', 'Fork2OverHeight3', '货叉2_超高3', '2', null, 'PLC', 'BOOL', 'DB101X34.6', '34.6', '1', '1', 'False', '无超高', '超高 3（货物高度与送货地址不匹配）', '2018-11-14 10:40:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('113', 'Fork2OverHeight', '货叉2_货物超高', '2', null, 'PLC', 'BOOL', 'DB101X34.7', '34.7', '1', '1', 'False', '无超高', '货物超高', '2018-11-14 10:40:50', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('114', 'Fork2PalletForkTimeout', '货叉2_货叉超时', '2', null, 'PLC', 'BOOL', 'DB101X35.0', '35.0', '1', '1', 'False', '无超时', '货叉超时', '2018-11-14 10:41:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('115', 'Fork2LeftLimitAlarm', '货叉2_是否左侧极限报警', '2', null, 'PLC', 'BOOL', 'DB101X35.1', '35.1', '1', '1', 'False', '无故障', '左侧极限报警', '2018-11-14 10:45:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('116', 'Fork2RightLimitAlarm', '货叉2_是否右侧极限报警', '2', null, 'PLC', 'BOOL', 'DB101X35.2', '35.2', '1', '1', 'False', '无故障', '右侧极限报警', '2018-11-14 10:47:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('117', 'Fork2ForkUuivertor', '货叉2_货叉变频器故障', '2', null, 'PLC', 'BOOL', 'DB101X35.3', '35.3', '1', '1', 'False', '无故障', '货叉变频器故障', '2018-11-14 10:48:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('118', 'Fork2ForkBreakerOrCocontactor', '货叉2_货叉断路器/接触器故障', '2', null, 'PLC', 'BOOL', 'DB101X35.4', '35.4', '1', '1', 'False', '无故障', '货叉断路器/接触器故障', '2018-11-14 10:49:14', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('119', 'Fork2GoodsInspectionSensor', '货叉2_是否货物检测传感器故障', '2', null, 'PLC', 'BOOL', 'DB101X35.5', '35.5', '1', '1', 'False', '无故障', '货物检测传感器故障', '2018-11-14 10:51:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('120', 'Fork2ForkAlignmentSensor', '货叉2_是否货叉定位传感器故障', '2', null, 'PLC', 'BOOL', 'DB101X35.6', '35.6', '1', '1', 'False', '无故障', '货叉定位传感器故障', '2018-11-14 10:52:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('121', 'Fork2DirectionError', '货叉2_是否运行方向错误', '2', null, 'PLC', 'BOOL', 'DB101X35.7', '35.7', '1', '1', 'False', '无故障', '运行方向错误', '2018-11-14 10:56:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('122', 'Fork2XYForkExcute', '货叉2_是否货叉执行动作错误', '2', null, 'PLC', 'BOOL', 'DB101X36.0', '36.0', '1', '1', 'False', '无故障', 'X轴、Y轴、货叉执行动作错误', '2018-11-14 10:58:48', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('123', 'Fork2SetValueError', '货叉2_是否设定值错误', '2', null, 'PLC', 'BOOL', 'DB101X36.1', '36.1', '1', '1', 'False', '无故障', '设定值错误', '2018-11-14 10:59:12', 'admin', '2018-11-22 10:07:38', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('124', 'Fork2PickupTaskError', '货叉2_是否取货任务错误', '2', '0-无故障；1-取货任务错误（取左2右2时，左1右1有货）', 'PLC', 'BOOL', 'DB101X36.2', '36.2', '1', '1', 'False', '无故障', '取货任务错误', '2018-11-14 10:59:28', 'admin', '2018-11-22 10:08:18', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('125', 'Fork2Spare3', '货叉2_Spare3', '2', null, 'PLC', 'BOOL', 'DB101X36.3', '36.3', '1', '0', null, null, null, '2018-11-14 10:59:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('126', 'Fork2DoubleIn', '货叉2_双重入库', '2', null, 'PLC', 'BOOL', 'DB101X36.4', '36.4', '1', '1', 'False', '无故障', '双重入库 满入', '2018-11-14 11:00:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('127', 'Fork2EmptyOut', '货叉2_是否空货位出库', '2', null, 'PLC', 'BOOL', 'DB101X36.5', '36.5', '1', '1', 'False', '无故障', '空货位出库 空出', '2018-11-14 11:01:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('128', 'Fork2ForkHasPallet', '货叉2_是否货叉有货', '2', null, 'PLC', 'BOOL', 'DB101X36.6', '36.6', '1', '1', 'False', '货叉无货', '货叉有货', '2018-11-14 11:03:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('129', 'Fork2ForkError', '货叉2_货叉总故障', '2', null, 'PLC', 'BOOL', 'DB101X36.7', '36.7', '1', '1', 'False', '无故障', '货叉故障', '2018-11-14 11:03:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('130', 'Fork2Spare4', '货叉2_Spare4', '2', null, 'PLC', 'BYTE', 'DB101B37', '37', '1', '0', null, null, null, '2018-11-14 11:04:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('131', 'Overload', '是否过载', '2', null, 'PLC', 'BOOL', 'DB101X38.0', '38.0', '1', '1', 'False', '无过载', '堆垛机故障', '2018-11-14 11:07:32', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('132', 'Rope', '是否松绳', '2', null, 'PLC', 'BOOL', 'DB101X38.1', '38.1', '1', '1', 'False', '无松绳', '松绳', '2018-11-14 11:08:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('133', 'RunningUuivertorAlarm', '是否行走变频器报警', '2', null, 'PLC', 'BOOL', 'DB101X38.2', '38.2', '1', '1', 'False', '无报警', '行走变频器报警', '2018-11-14 11:08:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('134', 'RaisingUuivertorAlarm', '是否起升变频器报警', '2', null, 'PLC', 'BOOL', 'DB101X38.3', '38.3', '1', '1', 'False', '无报警', '起升变频器报警', '2018-11-14 11:09:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('135', 'RunningTimeout', '是否运行超时', '2', null, 'PLC', 'BOOL', 'DB101X38.4', '38.4', '1', '1', 'False', '无超时', '运行超时', '2018-11-14 11:10:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('136', 'RaisingTimeout', '是否起升超时', '2', null, 'PLC', 'BOOL', 'DB101X38.5', '38.5', '1', '1', 'False', '无超时', '起升超时', '2018-11-14 11:10:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('137', 'HorizontalLaserDataError', '是否水平激光数据错误', '2', null, 'PLC', 'BOOL', 'DB101X38.6', '38.6', '1', '1', 'False', '无错误', '水平激光数据错误', '2018-11-14 11:11:23', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('138', 'RaisingBarcodeDataError', '起升条码数据错误', '2', null, 'PLC', 'BOOL', 'DB101X38.7', '38.7', '1', '1', 'False', '无错误', '起升条码数据错误', '2018-11-14 11:11:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('139', 'AdressError', '是否地址错', '2', null, 'PLC', 'BOOL', 'DB101X39.0', '39.0', '1', '1', 'False', '无故障', '地址错', '2018-11-14 11:12:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('140', 'MainCocontactorInterrupt', '主接触器断开', '2', '急停、冲 顶、超速保 护，行走超限动作', 'PLC', 'BOOL', 'DB101X39.1', '39.1', '1', '1', 'False', '无故障', '主接触器断开', '2018-11-14 11:14:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('141', 'HorizontalBreakerOrBrakeInterrupt', '水平断路器/制动器跳闸', '2', null, 'PLC', 'BOOL', 'DB101X39.2', '39.2', '1', '1', 'False', '无故障', '水平断路器/制动器跳闸', '2018-11-14 11:15:09', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('142', 'RaisingBreakerOrBrakeInterrupt', '是否起升断路器/制动器跳闸', '2', null, 'PLC', 'BOOL', 'DB101X39.3', '39.3', '1', '1', 'False', '无故障', '起升断路器/制动器跳闸', '2018-11-14 11:15:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('143', 'HorizontalLeadingendOut', '是否水平前端超限（前进终点）', '2', null, 'PLC', 'BOOL', 'DB101X39.4', '39.4', '1', '1', 'False', '无超限', '水平前端超限（前进终点）', '2018-11-14 11:16:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('144', 'HorizontalTrailingendOut', '是否水平后端超限（后退终点）', '2', null, 'PLC', 'BOOL', 'DB101X39.5', '39.5', '1', '1', 'False', '无超限', '水平后端超限（后退终点）', '2018-11-14 11:17:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('145', 'VerticalHorizontalLeadingendOut', '是否垂直上端超限（上升终点）', '2', null, 'PLC', 'BOOL', 'DB101X39.6', '39.6', '1', '1', 'False', '无超限', '垂直上端超限（上升终点）', '2018-11-14 11:18:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('146', 'VerticalHorizontalTrailingendOut', '垂直下端超限（下降终点）', '2', null, 'PLC', 'BOOL', 'DB101X39.7', '39.7', '1', '1', 'False', '无超限', '垂直下端超限（下降终点）', '2018-11-14 11:19:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('147', 'HorizontalUuivertorSpeed', '是否水平变频器速度超过设定值', '2', null, 'PLC', 'BOOL', 'DB101X40.0', '40.0', '1', '1', 'False', '无过载', '水平变频器速度超过设定值', '2018-11-14 11:21:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('148', 'RaisingUuivertorSpeed', '是否起升变频器速度超过设定值', '2', null, 'PLC', 'BOOL', 'DB101X40.1', '40.1', '1', '1', 'False', '无松绳', '起升变频器速度超过设定值', '2018-11-14 11:23:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('149', 'WCSForkAction', '货叉动作类型', '3', '0=无，1=1号货叉，2=2号货叉，3=同时动作', 'PLC', 'INT', 'DB100W0', '0', '1', '0', null, null, null, '2018-11-14 11:45:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('150', 'WCSFork1TaskFlag', '货叉1_任务标志', '3', '任务标志：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址, 6删除任务,10任务完成；任务完成时清除（由WCS清空为0）', 'PLC', 'INT', 'DB100W2', '2', '1', '0', null, null, null, '2018-11-14 11:46:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('151', 'WCSFork1Row', '货叉1_取放货地址:  行', '3', '取放货地址: 行  (1=左1，2=左2，3=右1，4=右2)', 'PLC', 'INT', 'DB100W4', '4', '1', '0', null, null, null, '2018-11-14 11:52:12', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('152', 'WCSFork1Column', '货叉1_取放货列', '3', '列（1-最远列）', 'PLC', 'INT', 'DB100W6', '6', '1', '0', null, null, null, '2018-11-14 11:53:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('153', 'WCSFork1Layer', '货叉1_取放货层', '3', '取放货地址: 层（1-最高层）', 'PLC', 'INT', 'DB100W8', '8', '1', '0', null, null, null, '2018-11-14 11:54:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('154', 'WCSFork1Station', '货叉1_取放货出入口', '3', '取放货出入口（1-10）', 'PLC', 'INT', 'DB100W10', '10', '1', '0', null, null, null, '2018-11-14 11:55:02', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('155', 'WCSFork1Task', '货叉1_任务号', '3', '货叉1任务号', 'PLC', 'DINT', 'DB100D22', '22', '1', '0', null, null, null, '2018-11-14 11:55:47', 'admin', '2018-11-23 15:06:09', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('156', 'WCSHeartBeat', '心跳', '3', null, 'PLC', 'INT', 'DB100W36', '36', '1', '0', null, null, null, '2018-11-14 11:56:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('157', 'WCSForkAction', '货叉动作类型', '2', '0=无，1=1号货叉，2=2号货叉，3=同时动作', 'PLC', 'INT', 'DB100W0', '0', '1', '0', null, null, null, '2018-11-14 11:45:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('158', 'WCSFork1TaskFlag', '货叉1_任务标志', '2', '任务标志：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址, 6删除任务,10任务完成；任务完成时清除（由WCS清空为0）', 'PLC', 'INT', 'DB100W2', '2', '1', '0', null, null, null, '2018-11-14 11:46:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('159', 'WCSFork1Row', '货叉1_取放货地址:  行', '2', '取放货地址: 行  (1=左1，2=左2，3=右1，4=右2)', 'PLC', 'INT', 'DB100W4', '4', '1', '0', null, null, null, '2018-11-14 11:52:12', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('160', 'WCSFork1Column', '货叉1_取放货列', '2', '列（1-最远列）', 'PLC', 'INT', 'DB100W6', '6', '1', '0', null, null, null, '2018-11-14 11:53:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('161', 'WCSFork1Layer', '货叉1_取放货层', '2', '取放货地址: 层（1-最高层）', 'PLC', 'INT', 'DB100W8', '8', '1', '0', null, null, null, '2018-11-14 11:54:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('162', 'WCSFork1Station', '货叉1_取放货出入口', '2', '取放货出入口（1-10）', 'PLC', 'INT', 'DB100W10', '10', '1', '0', null, null, null, '2018-11-14 11:55:02', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('163', 'WCSFork1Task', '货叉1_任务号', '2', '货叉1任务号', 'PLC', 'DINT', 'DB100D22', '22', '1', '0', null, null, null, '2018-11-14 11:55:47', 'admin', '2018-11-23 15:06:39', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('164', 'WCSFork2TaskFlag', '货叉2_任务标志', '2', '任务标志：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址, 6删除任务,10任务完成；任务完成时清除（由WCS清空为0）', 'PLC', 'INT', 'DB100W12', '12', '1', '0', null, null, null, '2018-11-14 11:46:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('165', 'WCSFork2Row', '货叉2_取放货地址:  行', '2', '取放货地址: 行  (1=左1，2=左2，3=右1，4=右2)', 'PLC', 'INT', 'DB100W14', '14', '1', '0', null, null, null, '2018-11-14 11:52:12', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('166', 'WCSFork2Column', '货叉2_取放货列', '2', '列（1-最远列）', 'PLC', 'INT', 'DB100W16', '16', '1', '0', null, null, null, '2018-11-14 11:53:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('167', 'WCSFork2Layer', '货叉2_取放货层', '2', '取放货地址: 层（1-最高层）', 'PLC', 'INT', 'DB100W18', '18', '1', '0', null, null, null, '2018-11-14 11:54:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('168', 'WCSFork2Station', '货叉2_取放货出入口', '2', '取放货出入口（1-10）', 'PLC', 'INT', 'DB100W20', '20', '1', '0', null, null, null, '2018-11-14 11:55:02', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('169', 'WCSFork2Task', '货叉2_任务号', '2', '货叉1任务号', 'PLC', 'DINT', 'DB100D26', '26', '1', '0', null, null, null, '2018-11-14 11:55:47', 'admin', '2018-11-23 15:06:45', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('170', 'WCSHeartBeat', '心跳', '2', null, 'PLC', 'INT', 'DB100W36', '36', '1', '0', null, null, null, '2018-11-14 11:56:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('171', 'RequestMessage', '地址请求', '4', '地址请求01', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('172', 'RequestLoadStatus', '地址请求-装载状态', '4', '2B', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('173', 'RequestNumber', '地址请求-读码器编号', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('174', 'RequestBarcode', '地址请求-条码', '4', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('175', 'RequestWeight', '地址请求-货物重量', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('176', 'RequestLength', '地址请求-货物长度', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('177', 'RequestWidth', '地址请求-货物宽度', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('178', 'RequestHeight', '地址请求-货物高度', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('179', 'RequestRetCode', '地址请求-RetCode', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('180', 'RequestBackup', '地址请求-备用', '4', null, 'PLC', 'CHAR', null, null, null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('181', 'ArriveMessage', '位置到达-报文', '4', '位置到达 02', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2018-11-14 13:58:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('182', 'ArriveResult', '位置到达-结果', '4', '1成功，2失败', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('183', 'ArriveRealAddress', '位置到达-实际到达地址', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('184', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('185', 'ArriveBarcode', '位置到达-条码', '4', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('186', 'ArrivePaddingBit', '位置到达-填充位', '4', '填充0', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('187', 'ControlMessage', '控制指令-报文', '4', '控制指令PLC-WCS 03', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:53:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('188', 'ControlType', '控制指令-类型', '4', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:55:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('189', 'ControlNumber', '控制指令-站台编号', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:56:03', 'admin', '2018-11-14 13:56:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('190', 'ControlBackUp', '控制指令-备用', '4', null, 'PLC', 'CHAR', null, null, null, '0', null, null, null, '2018-11-14 13:56:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('192', 'ACKMessage', 'ACK报文', '4', '05 PLC->WCS', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:59:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('193', 'ACKLoadStatus', 'ACK装载状态', '4', '回复WCS控制指令时，置2', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 13:59:58', 'admin', '2018-11-23 16:32:27', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('194', 'ACKNumber', 'ACK读码器编号', '4', '这个就是站台编号', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:00:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('195', 'ACKBackUp', 'ACK备用', '4', null, 'PLC', 'CHAR', null, null, null, '0', null, null, null, '2018-11-14 14:00:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('196', 'WCSReplyMessage', 'WCS地址回复报文', '4', '06 地址回复', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('197', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('198', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2018-11-14 14:04:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('199', 'WCSReplyBarcode', 'WCS地址回复-条码', '4', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('200', 'WCSReplyWeight', 'WCS地址回复-货物重量', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('201', 'WCSReplyLength', 'WCS地址回复-货物长度', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('202', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('203', 'WCSReplyHeight', 'WCS地址回复-货物高度', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('204', 'WCSReplyAddress', 'WCS地址回复-目标地址', '4', null, 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('205', 'WCSReplyBackUp', 'WCS地址回复-备用', '4', null, 'PLC', 'CHAR', null, null, null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('206', 'WCSControlMessage', 'WCS控制指令-报文', '4', '07 WCS-->PLC', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('207', 'WCSControlType', 'WCS控制指令-报文类型', '4', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('208', 'WCSControlNumber', 'WCS控制指令-读码器编号', '4', '站台编号', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('209', 'WCSControlBackup', 'WCS控制指令-备用', '4', null, 'PLC', 'CHAR', null, null, null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('210', 'WCSACKMessage', 'WCSACK报文', '4', '08 WCS-->PLC', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('211', 'WCSACKLoadStatus', 'WCSACK-装载状态', '4', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('212', 'WCSACKNumber', 'WCSACK-读码器编码', '4', '站台编号', 'PLC', 'INT', null, null, null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2018-11-14 14:17:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('213', 'WCSACKBackup', 'WCSACK-备用', '4', null, 'PLC', 'CHAR', null, null, null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('244', 'ArriveMessage', '位置到达-报文', '5', '位置到达 02', 'PLC', 'INT', null, '40', null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2019-07-19 15:51:34', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('245', 'ArriveResult', '位置到达-结果', '5', '1成功，2失败', 'PLC', 'INT', null, '42', null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', '2019-07-19 16:50:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('246', 'ArriveRealAddress', '位置到达-实际到达地址', '5', null, 'PLC', 'INT', null, '46', null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', '2019-07-19 16:50:23', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('247', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '5', null, 'PLC', 'INT', null, '44', null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', '2019-07-19 16:50:53', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('248', 'ArriveBarcode', '位置到达-条码', '5', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '48', null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', '2019-07-19 15:54:50', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('272', 'WCSACKMessage', 'WCSACK报文', '5', '08 WCS-->PLC', 'PLC', 'INT', null, '40', null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', '2019-07-19 16:08:27', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('273', 'WCSACKLoadStatus', 'WCSACK-装载状态', '5', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, '42', null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', '2019-07-19 16:08:43', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('274', 'WCSACKNumber', 'WCSACK-读码器编码', '5', '站台编号', 'PLC', 'INT', null, '44', null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2019-07-19 15:55:52', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('275', 'WCSACKBackup', 'WCSACK-备用', '5', null, 'PLC', 'INT', null, '46', null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', '2019-07-19 17:11:36', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('276', 'RequestMessage', '地址请求', '7', '地址请求01', 'PLC', 'INT', null, '0', null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', '2019-07-19 17:28:02', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('277', 'RequestLoadStatus', '地址请求-装载状态', '7', '2B', 'PLC', 'INT', null, '2', null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', '2019-07-19 17:28:11', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('278', 'RequestNumber', '地址请求-读码器编号', '7', null, 'PLC', 'INT', null, '4', null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', '2019-07-19 17:28:22', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('279', 'RequestBarcode', '地址请求-条码', '7', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '6', null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', '2019-07-19 17:28:31', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('280', 'RequestWeight', '地址请求-货物重量', '7', null, 'PLC', 'INT', null, '26', null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', '2019-07-19 17:28:41', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('281', 'RequestLength', '地址请求-货物长度', '7', null, 'PLC', 'INT', null, '28', null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', '2019-07-19 17:28:46', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('282', 'RequestWidth', '地址请求-货物宽度', '7', null, 'PLC', 'INT', null, '30', null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', '2019-07-19 17:28:52', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('283', 'RequestHeight', '地址请求-货物高度', '7', null, 'PLC', 'INT', null, '32', null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', '2019-07-19 17:28:57', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('284', 'RequestRetCode', '地址请求-RetCode', '7', null, 'PLC', 'INT', null, '34', null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', '2019-07-19 17:29:21', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('285', 'RequestBackup', '地址请求-备用', '7', null, 'PLC', 'INT', null, '36', null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', '2019-07-19 17:29:39', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('286', 'ArriveMessage', '位置到达-报文', '7', '位置到达 02', 'PLC', 'INT', null, '40', null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2019-07-19 17:29:52', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('287', 'ArriveResult', '位置到达-结果', '7', '1成功，2失败', 'PLC', 'INT', null, '42', null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', '2019-07-19 17:30:05', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('288', 'ArriveRealAddress', '位置到达-实际到达地址', '7', null, 'PLC', 'INT', null, '46', null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', '2019-07-19 17:30:18', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('289', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '7', null, 'PLC', 'INT', null, '44', null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', '2019-07-19 17:30:32', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('290', 'ArriveBarcode', '位置到达-条码', '7', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '48', null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', '2019-07-19 17:30:43', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('291', 'ArrivePaddingBit', '位置到达-填充位', '7', '填充0', 'PLC', 'INT', null, '68', null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', '2019-07-19 17:44:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('300', 'WCSReplyMessage', 'WCS地址回复报文', '7', '06 地址回复', 'PLC', 'INT', null, '0', null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', '2019-07-19 17:32:38', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('301', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '7', null, 'PLC', 'INT', null, '2', null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', '2019-07-19 17:32:44', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('302', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '7', null, 'PLC', 'INT', null, '4', null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2019-07-19 17:32:51', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('303', 'WCSReplyBarcode', 'WCS地址回复-条码', '7', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '6', null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', '2019-07-19 17:33:06', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('304', 'WCSReplyWeight', 'WCS地址回复-货物重量', '7', null, 'PLC', 'INT', null, '26', null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', '2019-07-19 17:33:20', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('305', 'WCSReplyLength', 'WCS地址回复-货物长度', '7', null, 'PLC', 'INT', null, '28', null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', '2019-07-19 17:33:28', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('306', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '7', null, 'PLC', 'INT', null, '30', null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', '2019-07-19 17:33:34', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('307', 'WCSReplyHeight', 'WCS地址回复-货物高度', '7', null, 'PLC', 'INT', null, '32', null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', '2019-07-19 17:33:41', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('308', 'WCSReplyAddress', 'WCS地址回复-目标地址', '7', null, 'PLC', 'INT', null, '34', null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', '2019-07-19 17:33:57', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('309', 'WCSReplyBackUp', 'WCS地址回复-备用', '7', null, 'PLC', 'INT', null, '36', null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', '2019-07-19 17:34:16', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('314', 'WCSACKMessage', 'WCSACK报文', '7', '08 WCS-->PLC', 'PLC', 'INT', null, '40', null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', '2019-07-19 17:34:34', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('315', 'WCSACKLoadStatus', 'WCSACK-装载状态', '7', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, '42', null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', '2019-07-19 17:34:45', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('316', 'WCSACKNumber', 'WCSACK-读码器编码', '7', '站台编号', 'PLC', 'INT', null, '44', null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2019-07-19 17:34:55', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('317', 'WCSACKBackup', 'WCSACK-备用', '7', null, 'PLC', 'INT', null, '46', null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', '2019-07-19 17:35:16', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('324', 'Fork2Distance', '货叉2伸叉测距', '2', '货叉2伸叉测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D20', '20', '1', '0', null, null, null, '2018-11-14 10:30:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('325', 'RequestMessage', '地址请求', '8', '地址请求01', 'PLC', 'INT', null, '0', null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', '2019-07-19 16:19:32', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('326', 'RequestLoadStatus', '地址请求-装载状态', '8', '2B', 'PLC', 'INT', null, '2', null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', '2019-07-19 16:19:41', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('327', 'RequestNumber', '地址请求-读码器编号', '8', null, 'PLC', 'INT', null, '4', null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', '2019-07-19 16:19:54', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('328', 'RequestBarcode', '地址请求-条码', '8', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '6', null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', '2019-07-19 16:20:04', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('329', 'RequestWeight', '地址请求-货物重量', '8', null, 'PLC', 'INT', null, '26', null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', '2019-07-19 16:20:28', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('330', 'RequestLength', '地址请求-货物长度', '8', null, 'PLC', 'INT', null, '28', null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', '2019-07-19 16:20:52', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('331', 'RequestWidth', '地址请求-货物宽度', '8', null, 'PLC', 'INT', null, '30', null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', '2019-07-19 16:21:05', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('332', 'RequestHeight', '地址请求-货物高度', '8', null, 'PLC', 'INT', null, '32', null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', '2019-07-19 16:21:51', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('333', 'RequestRetCode', '地址请求-RetCode', '8', null, 'PLC', 'INT', null, '34', null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', '2019-07-19 16:21:37', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('334', 'RequestBackup', '地址请求-备用', '8', null, 'PLC', 'INT', null, '36', null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', '2019-07-19 17:12:28', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('349', 'WCSReplyMessage', 'WCS地址回复报文', '8', '06 地址回复', 'PLC', 'INT', null, '0', null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', '2019-07-19 16:45:36', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('350', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '8', null, 'PLC', 'INT', null, '2', null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', '2019-07-19 16:45:50', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('351', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '8', null, 'PLC', 'INT', null, '4', null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2019-07-19 16:46:06', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('352', 'WCSReplyBarcode', 'WCS地址回复-条码', '8', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '6', null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', '2019-07-19 16:46:21', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('353', 'WCSReplyWeight', 'WCS地址回复-货物重量', '8', null, 'PLC', 'INT', null, '26', null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', '2019-07-19 16:46:43', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('354', 'WCSReplyLength', 'WCS地址回复-货物长度', '8', null, 'PLC', 'INT', null, '28', null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', '2019-07-19 16:46:50', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('355', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '8', null, 'PLC', 'INT', null, '30', null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', '2019-07-19 16:47:01', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('356', 'WCSReplyHeight', 'WCS地址回复-货物高度', '8', null, 'PLC', 'INT', null, '32', null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', '2019-07-19 16:47:33', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('357', 'WCSReplyAddress', 'WCS地址回复-目标地址', '8', null, 'PLC', 'INT', null, '34', null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', '2019-07-19 16:47:52', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('358', 'WCSReplyBackUp', 'WCS地址回复-备用', '8', null, 'PLC', 'INT', null, '36', null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', '2019-07-19 17:12:43', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('377', 'ArriveMessage', '位置到达-报文', '9', '位置到达 02', 'PLC', 'INT', null, '40', null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2019-07-19 17:19:01', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('378', 'ArriveResult', '位置到达-结果', '9', '1成功，2失败', 'PLC', 'INT', null, '42', null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', '2019-07-19 17:19:13', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('379', 'ArriveRealAddress', '位置到达-实际到达地址', '9', null, 'PLC', 'INT', null, '46', null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', '2019-07-19 17:19:47', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('380', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '9', null, 'PLC', 'INT', null, '44', null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', '2019-07-19 17:20:00', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('381', 'ArriveBarcode', '位置到达-条码', '9', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '48', null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', '2019-07-19 17:20:19', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('405', 'WCSACKMessage', 'WCSACK报文', '9', '08 WCS-->PLC', 'PLC', 'INT', null, '40', null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', '2019-07-19 17:20:35', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('406', 'WCSACKLoadStatus', 'WCSACK-装载状态', '9', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, '42', null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', '2019-07-19 17:20:43', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('407', 'WCSACKNumber', 'WCSACK-读码器编码', '9', '站台编号', 'PLC', 'INT', null, '44', null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2019-07-19 17:20:57', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('408', 'WCSACKBackup', 'WCSACK-备用', '9', null, 'PLC', 'INT', null, '46', null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', '2019-07-19 17:23:05', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('409', 'RequestMessage', '地址请求', '10', '地址请求01', 'PLC', 'INT', null, '0', null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', '2019-07-19 17:23:29', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('410', 'RequestLoadStatus', '地址请求-装载状态', '10', '2B', 'PLC', 'INT', null, '2', null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', '2019-07-19 17:23:35', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('411', 'RequestNumber', '地址请求-读码器编号', '10', null, 'PLC', 'INT', null, '4', null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', '2019-07-19 17:23:41', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('412', 'RequestBarcode', '地址请求-条码', '10', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '6', null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', '2019-07-19 17:23:49', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('413', 'RequestWeight', '地址请求-货物重量', '10', null, 'PLC', 'INT', null, '26', null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', '2019-07-19 17:24:19', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('414', 'RequestLength', '地址请求-货物长度', '10', null, 'PLC', 'INT', null, '28', null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', '2019-07-19 17:24:25', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('415', 'RequestWidth', '地址请求-货物宽度', '10', null, 'PLC', 'INT', null, '30', null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', '2019-07-19 17:24:37', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('416', 'RequestHeight', '地址请求-货物高度', '10', null, 'PLC', 'INT', null, '32', null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', '2019-07-19 17:24:43', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('417', 'RequestRetCode', '地址请求-RetCode', '10', null, 'PLC', 'INT', null, '34', null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', '2019-07-19 17:24:59', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('418', 'RequestBackup', '地址请求-备用', '10', null, 'PLC', 'INT', null, '36', null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', '2019-07-19 17:27:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('433', 'WCSReplyMessage', 'WCS地址回复报文', '10', '06 地址回复', 'PLC', 'INT', null, '0', null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', '2019-07-19 17:25:26', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('434', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '10', null, 'PLC', 'INT', null, '2', null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', '2019-07-19 17:25:37', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('435', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '10', null, 'PLC', 'INT', null, '4', null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2019-07-19 17:25:46', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('436', 'WCSReplyBarcode', 'WCS地址回复-条码', '10', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '6', null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', '2019-07-19 17:25:57', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('437', 'WCSReplyWeight', 'WCS地址回复-货物重量', '10', null, 'PLC', 'INT', null, '26', null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', '2019-07-19 17:26:08', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('438', 'WCSReplyLength', 'WCS地址回复-货物长度', '10', null, 'PLC', 'INT', null, '28', null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', '2019-07-19 17:26:14', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('439', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '10', null, 'PLC', 'INT', null, '30', null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', '2019-07-19 17:26:22', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('440', 'WCSReplyHeight', 'WCS地址回复-货物高度', '10', null, 'PLC', 'INT', null, '32', null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', '2019-07-19 17:26:34', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('441', 'WCSReplyAddress', 'WCS地址回复-目标地址', '10', null, 'PLC', 'INT', null, '34', null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', '2019-07-19 17:26:44', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('442', 'WCSReplyBackUp', 'WCS地址回复-备用', '10', null, 'PLC', 'INT', null, '36', null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', '2019-07-19 17:26:58', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('451', 'RequestMessage', '地址请求', '11', '地址请求01', 'PLC', 'INT', null, '0', null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', '2019-07-19 16:29:21', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('452', 'RequestLoadStatus', '地址请求-装载状态', '11', '2B', 'PLC', 'INT', null, '2', null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', '2019-07-19 16:29:33', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('453', 'RequestNumber', '地址请求-读码器编号', '11', null, 'PLC', 'INT', null, '4', null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', '2019-07-19 16:29:45', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('454', 'RequestBarcode', '地址请求-条码', '11', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '6', null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', '2019-07-19 16:29:58', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('455', 'RequestWeight', '地址请求-货物重量', '11', null, 'PLC', 'INT', null, '26', null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', '2019-07-19 16:30:20', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('456', 'RequestLength', '地址请求-货物长度', '11', null, 'PLC', 'INT', null, '28', null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', '2019-07-19 16:30:31', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('457', 'RequestWidth', '地址请求-货物宽度', '11', null, 'PLC', 'INT', null, '30', null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', '2019-07-19 16:30:43', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('458', 'RequestHeight', '地址请求-货物高度', '11', null, 'PLC', 'INT', null, '32', null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', '2019-07-19 16:30:55', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('459', 'RequestRetCode', '地址请求-RetCode', '11', null, 'PLC', 'INT', null, '34', null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', '2019-07-19 16:31:06', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('460', 'RequestBackup', '地址请求-备用', '11', null, 'PLC', 'INT', null, '36', null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', '2019-07-19 17:17:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('461', 'ArriveMessage', '位置到达-报文', '11', '位置到达 02', 'PLC', 'INT', null, '40', null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2019-07-19 16:32:08', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('462', 'ArriveResult', '位置到达-结果', '11', '1成功，2失败', 'PLC', 'INT', null, '42', null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', '2019-07-19 16:32:20', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('463', 'ArriveRealAddress', '位置到达-实际到达地址', '11', null, 'PLC', 'INT', null, '46', null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', '2019-07-19 16:32:37', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('464', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '11', null, 'PLC', 'INT', null, '44', null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', '2019-07-19 16:38:47', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('465', 'ArriveBarcode', '位置到达-条码', '11', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '48', null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', '2019-07-19 16:32:56', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('475', 'WCSReplyMessage', 'WCS地址回复报文', '11', '06 地址回复', 'PLC', 'INT', null, '0', null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', '2019-07-19 16:33:54', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('476', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '11', null, 'PLC', 'INT', null, '2', null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', '2019-07-19 16:34:54', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('477', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '11', null, 'PLC', 'INT', null, '4', null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2019-07-19 16:35:08', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('478', 'WCSReplyBarcode', 'WCS地址回复-条码', '11', 'PLC上报的条码信息', 'PLC', 'CHAR', null, '6', null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', '2019-07-19 16:35:21', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('479', 'WCSReplyWeight', 'WCS地址回复-货物重量', '11', null, 'PLC', 'INT', null, '26', null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', '2019-07-19 16:36:05', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('480', 'WCSReplyLength', 'WCS地址回复-货物长度', '11', null, 'PLC', 'INT', null, '28', null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', '2019-07-19 16:36:13', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('481', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '11', null, 'PLC', 'INT', null, '30', null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', '2019-07-19 16:36:22', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('482', 'WCSReplyHeight', 'WCS地址回复-货物高度', '11', null, 'PLC', 'INT', null, '32', null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', '2019-07-19 16:36:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('483', 'WCSReplyAddress', 'WCS地址回复-目标地址', '11', null, 'PLC', 'INT', null, '34', null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', '2019-07-19 16:41:57', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('484', 'WCSReplyBackUp', 'WCS地址回复-备用', '11', null, 'PLC', 'INT', null, '36', null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', '2019-07-19 17:17:20', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('485', 'WCSControlMessage', 'WCS控制指令-报文', '11', '07 WCS-->PLC', 'PLC', 'INT', null, '40', null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', '2019-07-19 16:39:51', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('486', 'WCSControlType', 'WCS控制指令-报文类型', '11', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, '42', null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', '2019-07-19 17:22:39', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('487', 'WCSControlNumber', 'WCS控制指令-读码器编号', '11', '站台编号', 'PLC', 'INT', null, '44', null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', '2019-07-19 16:40:12', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('488', 'WCSControlBackup', 'WCS控制指令-备用', '11', null, 'PLC', 'INT', null, '46', null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', '2019-07-19 17:17:43', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('496', 'Number', '堆垛机编号', '12', '堆垛机编号', 'PLC', 'INT', 'DB101W0', null, null, '0', null, null, null, '2018-11-14 10:11:39', 'admin', '2019-07-09 19:39:01', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('497', 'OperationModel', '操作模式', '12', '操作模式：1-维修；2-手动；3-机载操作；4-单机自动；5-联机', 'PLC', 'INT', 'DB101W2', null, null, '0', null, null, null, '2018-11-14 10:12:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('498', 'HeartBeat', '心跳', '12', null, 'PLC', 'INT', 'DB101W4', null, null, '0', null, null, null, '2018-11-14 10:13:24', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('499', 'TaskLimit', '任务限制', '12', '任务限制：1-无限制，2-入库限制，3-出库限制', 'PLC', 'INT', 'DB101W6', null, null, '0', null, null, null, '2018-11-14 10:14:12', 'admin', '2018-11-14 10:16:13', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('500', 'Fork1TaskExcuteStatus', '货叉1_任务执行', '12', '1-待机；2-任务执行中;3-任务完成；4-任务中断（出错）；5-下发任务错误', 'PLC', 'INT', 'DB101W42', null, null, '0', null, null, null, '2018-11-14 10:20:26', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('501', 'Fork1TaskNo', '货叉1_任务号', '12', '任务号', 'PLC', 'DINT', 'DB101D46', null, null, '0', null, null, null, '2018-11-14 10:21:18', 'admin', '2018-11-23 14:57:03', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('502', 'Fork1TaskType', '货叉1_任务类型', '12', '货叉任务类型：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库', 'PLC', 'INT', 'DB101W54', null, null, '0', null, null, null, '2018-11-14 10:23:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('503', 'HorizontalDistance', '水平测距', '12', '水平测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D8', null, null, '0', null, null, null, '2018-11-14 10:27:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('504', 'VerticalDistance', '起升测距', '12', '起升测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D12', null, null, '0', null, null, null, '2018-11-14 10:28:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('505', 'For1kDistance', '货叉1伸叉测距', '12', '货叉1伸叉测距数据 单位 1mm', 'PLC', 'DINT', 'DB101D16', null, null, '0', null, null, null, '2018-11-14 10:30:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('506', 'CurrentColumn', '当前列', '12', null, 'PLC', 'INT', 'DB101W24', null, null, '0', null, null, null, '2018-11-14 10:30:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('507', 'CurrentLayer', '当前层', '12', null, 'PLC', 'INT', 'DB101W26', null, null, '0', null, null, null, '2018-11-14 10:31:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('508', 'CurrentStation', '当前出/入口', '12', '当前出/入口 1-10', 'PLC', 'INT', 'DB101W28', null, null, '0', null, null, null, '2018-11-14 10:32:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('509', 'Fork1FrontOut', '货叉1_是否货物前超', '12', '-无超限；1-货物前超', 'PLC', 'BOOL', 'DB101X30.0', null, null, '1', 'False', '无超限', '货物前超', '2018-11-14 10:34:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('510', 'Fork1BehindOut', '货叉1_是否货物后超', '12', null, 'PLC', 'BOOL', 'DB101X30.1', null, null, '1', 'False', '无超限', '货物后超', '2018-11-14 10:35:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('511', 'Fork1LeftForkOut', '货叉1_是否左侧外形超限', '12', null, 'PLC', 'BOOL', 'DB101X30.2', null, null, '1', 'False', '无超限', '左侧外形超限', '2018-11-14 10:37:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('512', 'Fork1RightForkOut', '货叉1_是否右侧外形超限', '12', null, 'PLC', 'BOOL', 'DB101X30.3', null, null, '1', 'False', '无超限', '右侧外形超限', '2018-11-14 10:38:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('513', 'Fork1OverHeight1', '货叉1_超高1', '12', null, 'PLC', 'BOOL', 'DB101X30.4', null, null, '1', 'False', '无超高', '超高 1（货物高度与送货地址不匹配）', '2018-11-14 10:39:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('514', 'Fork1OverHeight2', '货叉1_超高2', '12', null, 'PLC', 'BOOL', 'DB101X30.5', null, null, '1', 'False', '无超高', '超高 2（货物高度与送货地址不匹配）', '2018-11-14 10:39:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('515', 'Fork1OverHeight3', '货叉1_超高3', '12', null, 'PLC', 'BOOL', 'DB101X30.6', null, null, '1', 'False', '无超高', '超高 3（货物高度与送货地址不匹配）', '2018-11-14 10:40:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('516', 'Fork1OverHeight', '货叉1_货物超高', '12', null, 'PLC', 'BOOL', 'DB101X30.7', null, null, '1', 'False', '无超高', '货物超高', '2018-11-14 10:40:50', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('517', 'Fork1PalletForkTimeout', '货叉1_货叉超时', '12', null, 'PLC', 'BOOL', 'DB101X31.0', null, null, '1', 'False', '无超时', '货叉超时', '2018-11-14 10:41:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('518', 'Fork1LeftLimitAlarm', '货叉1_是否左侧极限报警', '12', null, 'PLC', 'BOOL', 'DB101X31.1', null, null, '1', 'False', '无故障', '左侧极限报警', '2018-11-14 10:45:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('519', 'Fork1RightLimitAlarm', '货叉1_是否右侧极限报警', '12', null, 'PLC', 'BOOL', 'DB101X31.2', null, null, '1', 'False', '无故障', '右侧极限报警', '2018-11-14 10:47:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('520', 'Fork1ForkUuivertor', '货叉1_货叉变频器故障', '12', null, 'PLC', 'BOOL', 'DB101X31.3', null, null, '1', 'False', '无故障', '货叉变频器故障', '2018-11-14 10:48:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('521', 'Fork1ForkBreakerOrCocontactor', '货叉1_货叉断路器/接触器故障', '12', null, 'PLC', 'BOOL', 'DB101X31.4', null, null, '1', 'False', '无故障', '货叉断路器/接触器故障', '2018-11-14 10:49:14', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('522', 'Fork1GoodsInspectionSensor', '货叉1_是否货物检测传感器故障', '12', null, 'PLC', 'BOOL', 'DB101X31.5', null, null, '1', 'False', '无故障', '货物检测传感器故障', '2018-11-14 10:51:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('523', 'Fork1ForkAlignmentSensor', '货叉1_是否货叉定位传感器故障', '12', null, 'PLC', 'BOOL', 'DB101X31.6', null, null, '1', 'False', '无故障', '货叉定位传感器故障', '2018-11-14 10:52:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('524', 'Fork1DirectionError', '货叉1_是否运行方向错误', '12', null, 'PLC', 'BOOL', 'DB101X31.7', null, null, '1', 'False', '无故障', '运行方向错误', '2018-11-14 10:56:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('525', 'Fork1XYForkExcute', '货叉1_是否货叉执行动作错误', '12', null, 'PLC', 'BOOL', 'DB101X32.0', null, null, '1', 'False', '无故障', 'X轴、Y轴、货叉执行动作错误', '2018-11-14 10:58:48', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('526', 'Fork1SetValueError', '货叉1_是否设定值错误', '12', null, 'PLC', 'BOOL', 'DB101X32.1', null, null, '1', 'False', '无故障', '设定值错误', '2018-11-14 10:59:12', 'admin', '2018-11-22 10:02:58', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('527', 'Fork1PickupTaskError', '货叉1_是否取货任务错误', '12', '0-无故障；1-取货任务错误（取左2右2时，左1右1有货）', 'PLC', 'BOOL', 'DB101X58.0', null, null, '1', 'False', '无故障', '取货任务错误', '2018-11-14 10:59:28', 'admin', '2019-07-09 20:30:09', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('529', 'Fork1DoubleIn', '货叉1_双重入库', '12', null, 'PLC', 'BOOL', 'DB101X32.4', null, null, '1', 'False', '无故障', '双重入库 满入', '2018-11-14 11:00:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('530', 'Fork1EmptyOut', '货叉1_是否空货位出库', '12', null, 'PLC', 'BOOL', 'DB101X32.5', null, null, '1', 'False', '无故障', '空货位出库 空出', '2018-11-14 11:01:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('531', 'Fork1ForkHasPallet', '货叉1_是否货叉有货', '12', null, 'PLC', 'BOOL', 'DB101X32.6', null, null, '0', 'False', '货叉无货', '货叉有货', '2018-11-14 11:03:10', 'admin', '2018-11-19 19:22:32', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('532', 'Fork1ForkError', '货叉1_货叉总故障', '12', null, 'PLC', 'BOOL', 'DB101X32.7', null, null, '1', 'False', '无故障', '货叉故障', '2018-11-14 11:03:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('534', 'Overload', '是否过载', '12', null, 'PLC', 'BOOL', 'DB101X38.0', null, null, '1', 'False', '无过载', '堆垛机故障', '2018-11-14 11:07:32', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('535', 'Rope', '是否松绳', '12', null, 'PLC', 'BOOL', 'DB101X38.1', null, null, '1', 'False', '无松绳', '松绳', '2018-11-14 11:08:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('536', 'RunningUuivertorAlarm', '是否行走变频器报警', '12', null, 'PLC', 'BOOL', 'DB101X38.2', null, null, '1', 'False', '无报警', '行走变频器报警', '2018-11-14 11:08:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('537', 'RaisingUuivertorAlarm', '是否起升变频器报警', '12', null, 'PLC', 'BOOL', 'DB101X38.3', null, null, '1', 'False', '无报警', '起升变频器报警', '2018-11-14 11:09:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('538', 'RunningTimeout', '是否运行超时', '12', null, 'PLC', 'BOOL', 'DB101X38.4', null, null, '1', 'False', '无超时', '运行超时', '2018-11-14 11:10:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('539', 'RaisingTimeout', '是否起升超时', '12', null, 'PLC', 'BOOL', 'DB101X38.5', null, null, '1', 'False', '无超时', '起升超时', '2018-11-14 11:10:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('540', 'HorizontalLaserDataError', '是否水平激光数据错误', '12', null, 'PLC', 'BOOL', 'DB101X38.6', null, null, '1', 'False', '无错误', '水平激光数据错误', '2018-11-14 11:11:23', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('541', 'RaisingBarcodeDataError', '起升条码数据错误', '12', null, 'PLC', 'BOOL', 'DB101X38.7', null, null, '1', 'False', '无错误', '起升条码数据错误', '2018-11-14 11:11:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('542', 'AdressError', '是否地址错', '12', null, 'PLC', 'BOOL', 'DB101X39.0', null, null, '1', 'False', '无故障', '地址错', '2018-11-14 11:12:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('543', 'MainCocontactorInterrupt', '主接触器断开', '12', '急停、冲 顶、超速保 护，行走超限动作', 'PLC', 'BOOL', 'DB101X39.1', null, null, '1', 'False', '无故障', '主接触器断开', '2018-11-14 11:14:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('544', 'HorizontalBreakerOrBrakeInterrupt', '水平断路器/制动器跳闸', '12', null, 'PLC', 'BOOL', 'DB101X39.2', null, null, '1', 'False', '无故障', '水平断路器/制动器跳闸', '2018-11-14 11:15:09', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('545', 'RaisingBreakerOrBrakeInterrupt', '是否起升断路器/制动器跳闸', '12', null, 'PLC', 'BOOL', 'DB101X39.3', null, null, '1', 'False', '无故障', '起升断路器/制动器跳闸', '2018-11-14 11:15:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('546', 'HorizontalLeadingendOut', '是否水平前端超限（前进终点）', '12', null, 'PLC', 'BOOL', 'DB101X39.4', null, null, '1', 'False', '无超限', '水平前端超限（前进终点）', '2018-11-14 11:16:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('547', 'HorizontalTrailingendOut', '是否水平后端超限（后退终点）', '12', null, 'PLC', 'BOOL', 'DB101X39.5', null, null, '1', 'False', '无超限', '水平后端超限（后退终点）', '2018-11-14 11:17:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('548', 'VerticalHorizontalLeadingendOut', '是否垂直上端超限（上升终点）', '12', null, 'PLC', 'BOOL', 'DB101X39.6', null, null, '1', 'False', '无超限', '垂直上端超限（上升终点）', '2018-11-14 11:18:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('549', 'VerticalHorizontalTrailingendOut', '垂直下端超限（下降终点）', '12', null, 'PLC', 'BOOL', 'DB101X39.7', null, null, '1', 'False', '无超限', '垂直下端超限（下降终点）', '2018-11-14 11:19:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('550', 'HorizontalUuivertorSpeed', '是否水平变频器速度超过设定值', '12', null, 'PLC', 'BOOL', 'DB101X40.1', null, null, '1', 'False', '无过载', '水平变频器速度超过设定值', '2018-11-14 11:21:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('551', 'RaisingUuivertorSpeed', '是否起升变频器速度超过设定值', '12', null, 'PLC', 'BOOL', 'DB101X40.2', null, null, '1', 'False', '无松绳', '起升变频器速度超过设定值', '2018-11-14 11:23:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('552', 'Fork1PickDownTaskError', '货叉1_是否放货任务错误', '12', null, 'PLC', 'BOOL', 'DB101X58.1', null, null, '1', 'False', '无错误', '货叉1_放货错误', '2019-07-09 20:28:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('553', 'Fork1RowError', '货叉1_下发任务排错误', '12', '0-无错误；1-WCS下发任务时，任务排号错误', 'PLC', 'BOOL', 'DB101X58.2', null, null, '1', 'False', '无错误', '下发任务排错误', '2019-07-09 20:33:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('554', 'Fork1LayerError', '货叉1_下发任务层错误', '12', '0-无错误；1-WCS下发任务时，任务层号错误', 'PLC', 'BOOL', 'DB101X58.3', null, null, '1', 'False', '无错误', '下发任务排错误', '2019-07-09 20:33:21', 'admin', '2019-07-09 20:33:21', '');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('555', 'Fork1LineError', '货叉1_下发任务列错误', '12', '0-无错误；1-WCS下发任务时，任务列号错误', 'PLC', 'BOOL', 'DB101X58.4', null, null, '1', 'False', '无错误', '下发任务排错误', '2019-07-09 20:33:21', 'admin', '2019-07-09 20:33:21', '');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('556', 'Fork1StationError', '货叉1_下发任务出入口错误', '12', '0-无错误；1-WCS下发任务时，任务出入口错误', 'PLC', 'BOOL', 'DB101X58.5', null, null, '1', 'False', '无错误', '下发任务排错误', '2019-07-09 20:33:21', 'admin', '2019-07-09 20:33:21', '');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('557', 'Fork1HeightMatchError', '货叉1_下发任务货物高度与货柜不匹配', '12', '0-无错误；1-库内放货，货物高度与货架高度不匹配', 'PLC', 'BOOL', 'DB101X58.6', null, null, '1', 'False', '无错误', '下发任务排错误', '2019-07-09 20:33:21', 'admin', '2019-07-09 20:33:21', '');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('558', 'WCSForkTaskMode', '货叉任务模型', '12', '0=无，1=完整任务，2=部分任务', 'PLC', 'INT', 'DB100W0', null, null, '0', null, null, null, '2018-11-14 11:45:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('559', 'WCSForkAction', '货叉动作类型', '12', '0=无，1=1号货叉，2=2号货叉，3=同时动作', 'PLC', 'INT', 'DB100W40', null, null, '0', null, null, null, '2018-11-14 11:45:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('560', 'WCSFork1TaskFlag', '货叉1_任务标志', '12', '任务标志：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址, 6删除任务,10任务完成；任务完成时清除（由WCS清空为0）', 'PLC', 'INT', 'DB100W2', null, null, '0', null, null, null, '2018-11-14 11:46:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('561', 'WCSFork1Row', '货叉1_取货地址:  行', '12', '取货地址: 行  (1=左1，2=左2，3=右1，4=右2)', 'PLC', 'INT', 'DB100W4', null, null, '0', null, null, null, '2018-11-14 11:52:12', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('562', 'WCSFork1Column', '货叉1_取货列', '12', '列（1-最远列）', 'PLC', 'INT', 'DB100W8', null, null, '0', null, null, null, '2018-11-14 11:53:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('563', 'WCSFork1Layer', '货叉1_取货层', '12', '取放货地址: 层（1-最高层）', 'PLC', 'INT', 'DB100W10', null, null, '0', null, null, null, '2018-11-14 11:54:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('564', 'WCSFork1Station', '货叉1_取货出入口', '12', '取放货出入口（1-10）', 'PLC', 'INT', 'DB100W6', null, null, '0', null, null, null, '2018-11-14 11:55:02', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('565', 'WCSFork1TaskFlag2', '货叉2_任务标志', '12', '0-无任务，2-库内放货，4-库外放货，5重新分配入库地址', 'PLC', 'INT', 'DB100W12', null, null, '0', null, null, null, '2018-11-14 11:46:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('566', 'WCSFork1Row2', '货叉2_放货地址:  行', '12', '放货地址: 行  (1=左1，2=左2，3=右1，4=右2)', 'PLC', 'INT', 'DB100W14', null, null, '0', null, null, null, '2018-11-14 11:52:12', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('567', 'WCSFork1Column2', '货叉2_放货列', '12', '列（1-最远列）', 'PLC', 'INT', 'DB100W18', null, null, '0', null, null, null, '2018-11-14 11:53:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('568', 'WCSFork1Layer2', '货叉2_放货层', '12', '放货地址: 层（1-最高层）', 'PLC', 'INT', 'DB100W20', null, null, '0', null, null, null, '2018-11-14 11:54:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('569', 'WCSFork1Station2', '货叉2_放货出入口', '12', '放货出入口（1-10）', 'PLC', 'INT', 'DB100W16', null, null, '0', null, null, null, '2018-11-14 11:55:02', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('570', 'WCSFork1Task', '货叉1_任务号', '12', '货叉1任务号', 'PLC', 'DINT', 'DB100D22', null, null, '0', null, null, null, '2018-11-14 11:55:47', 'admin', '2018-11-23 15:06:09', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('571', 'WCSTaskAccount', '任务过账', '12', '0=未收到任务完成，10=任务完成已接收', 'PLC', 'DINT', 'DB100W30', null, null, '0', null, null, null, '2018-11-14 11:56:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate` VALUES ('572', 'WCSHeartBeat', '心跳', '12', '心跳 累加 1-32767', 'PLC', 'INT', 'DB100W38', null, null, '0', null, null, null, '2018-11-14 11:56:13', 'admin', null, null);

-- ----------------------------
-- Table structure for wcsequipmenttypeproptemplate2
-- ----------------------------
DROP TABLE IF EXISTS `wcsequipmenttypeproptemplate2`;
CREATE TABLE `wcsequipmenttypeproptemplate2` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) COLLATE utf8_bin NOT NULL,
  `name` varchar(50) COLLATE utf8_bin NOT NULL,
  `equipmentTypeId` int(11) NOT NULL,
  `description` varchar(200) COLLATE utf8_bin DEFAULT NULL,
  `propType` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '指定该属性的类型，比如设备固有属性、PLC地址属性等',
  `dataType` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '属性的数据类型，用作给PLC传值的时候转换数据类型',
  `offset` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '偏移值',
  `address` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `isMonitor` tinyint(4) NOT NULL,
  `monitorCompareValue` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `monitorNormal` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `monitorFailure` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=579 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsequipmenttypeproptemplate2
-- ----------------------------
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('9', 'Number', '堆垛机编号', '3', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:11:39', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('10', 'OperationModel', '操作模式', '3', '操作模式：1-维修；2-手动；3-机载操作；4-单机自动；5-联机', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:12:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('11', 'HeartBeat', '心跳', '3', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:13:24', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('12', 'TaskLimit', '任务限制', '3', '任务限制：1-无限制，2-入库限制，3-出库限制', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:14:12', 'admin', '2018-11-14 10:16:13', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('13', 'Fork1TaskExcuteStatus', '货叉1_任务执行', '3', '1-待机；2-任务执行中;3-任务完成；4-任务中断（出错）；5-下发任务错误', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:20:26', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('14', 'Fork1TaskNo', '货叉1_任务号', '3', '任务号', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:21:18', 'admin', '2018-11-23 14:57:03', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('15', 'Fork1TaskType', '货叉1_任务类型', '3', '货叉任务类型：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:23:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('16', 'HorizontalDistance', '水平测距', '3', '水平测距数据 单位 1mm', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:27:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('17', 'VerticalDistance', '起升测距', '3', '起升测距数据 单位 1mm', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:28:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('18', 'For1kDistance', '货叉1伸叉测距', '3', '货叉1伸叉测距数据 单位 1mm', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:30:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('19', 'CurrentColumn', '当前列', '3', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:30:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('20', 'CurrentLayer', '当前层', '3', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:31:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('21', 'CurrentStation', '当前出/入口', '3', '当前出/入口 1-10', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:32:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('22', 'Fork1FrontOut', '货叉1_是否货物前超', '3', '-无超限；1-货物前超', 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '货物前超', '2018-11-14 10:34:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('23', 'Fork1BehindOut', '货叉1_是否货物后超', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '货物后超', '2018-11-14 10:35:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('24', 'Fork1LeftForkOut', '货叉1_是否左侧外形超限', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '左侧外形超限', '2018-11-14 10:37:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('25', 'Fork1RightForkOut', '货叉1_是否右侧外形超限', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '右侧外形超限', '2018-11-14 10:38:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('26', 'Fork1OverHeight1', '货叉1_超高1', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '超高 1（货物高度与送货地址不匹配）', '2018-11-14 10:39:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('27', 'Fork1OverHeight2', '货叉1_超高2', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '超高 2（货物高度与送货地址不匹配）', '2018-11-14 10:39:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('28', 'Fork1OverHeight3', '货叉1_超高3', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '超高 3（货物高度与送货地址不匹配）', '2018-11-14 10:40:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('29', 'Fork1OverHeight', '货叉1_货物超高', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '货物超高', '2018-11-14 10:40:50', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('30', 'Fork1PalletForkTimeout', '货叉1_货叉超时', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超时', '货叉超时', '2018-11-14 10:41:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('31', 'Fork1LeftLimitAlarm', '货叉1_是否左侧极限报警', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '左侧极限报警', '2018-11-14 10:45:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('32', 'Fork1RightLimitAlarm', '货叉1_是否右侧极限报警', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '右侧极限报警', '2018-11-14 10:47:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('33', 'Fork1ForkUuivertor', '货叉1_货叉变频器故障', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉变频器故障', '2018-11-14 10:48:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('34', 'Fork1ForkBreakerOrCocontactor', '货叉1_货叉断路器/接触器故障', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉断路器/接触器故障', '2018-11-14 10:49:14', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('35', 'Fork1GoodsInspectionSensor', '货叉1_是否货物检测传感器故障', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货物检测传感器故障', '2018-11-14 10:51:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('36', 'Fork1ForkAlignmentSensor', '货叉1_是否货叉定位传感器故障', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉定位传感器故障', '2018-11-14 10:52:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('37', 'Fork1DirectionError', '货叉1_是否运行方向错误', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '运行方向错误', '2018-11-14 10:56:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('38', 'Fork1XYForkExcute', '货叉1_是否货叉执行动作错误', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', 'X轴、Y轴、货叉执行动作错误', '2018-11-14 10:58:48', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('39', 'Fork1SetValueError', '货叉1_是否设定值错误', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '设定值错误', '2018-11-14 10:59:12', 'admin', '2018-11-22 10:02:58', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('40', 'Fork1PickupTaskError', '货叉1_是否取货任务错误', '3', '0-无故障；1-取货任务错误（取左2右2时，左1右1有货）', 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '取货任务错误', '2018-11-14 10:59:28', 'admin', '2018-11-22 10:03:58', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('41', 'Fork1Spare3', '货叉1_Spare3', '3', null, 'PLC', 'BOOL', null, null, '0', null, null, null, '2018-11-14 10:59:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('42', 'Fork1DoubleIn', '货叉1_双重入库', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '双重入库 满入', '2018-11-14 11:00:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('43', 'Fork1EmptyOut', '货叉1_是否空货位出库', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '空货位出库 空出', '2018-11-14 11:01:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('44', 'Fork1ForkHasPallet', '货叉1_是否货叉有货', '3', null, 'PLC', 'BOOL', null, null, '0', 'False', '货叉无货', '货叉有货', '2018-11-14 11:03:10', 'admin', '2018-11-19 19:22:32', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('45', 'Fork1ForkError', '货叉1_货叉总故障', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉故障', '2018-11-14 11:03:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('46', 'Fork1Spare4', '货叉1_Spare4', '3', null, 'PLC', 'BYTE', null, null, '0', null, null, null, '2018-11-14 11:04:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('47', 'Overload', '是否过载', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无过载', '堆垛机故障', '2018-11-14 11:07:32', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('48', 'Rope', '是否松绳', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无松绳', '松绳', '2018-11-14 11:08:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('49', 'RunningUuivertorAlarm', '是否行走变频器报警', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无报警', '行走变频器报警', '2018-11-14 11:08:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('50', 'RaisingUuivertorAlarm', '是否起升变频器报警', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无报警', '起升变频器报警', '2018-11-14 11:09:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('51', 'RunningTimeout', '是否运行超时', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超时', '运行超时', '2018-11-14 11:10:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('52', 'RaisingTimeout', '是否起升超时', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超时', '起升超时', '2018-11-14 11:10:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('53', 'HorizontalLaserDataError', '是否水平激光数据错误', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无错误', '水平激光数据错误', '2018-11-14 11:11:23', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('54', 'RaisingBarcodeDataError', '起升条码数据错误', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无错误', '起升条码数据错误', '2018-11-14 11:11:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('55', 'AdressError', '是否地址错', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '地址错', '2018-11-14 11:12:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('56', 'MainCocontactorInterrupt', '主接触器断开', '3', '急停、冲 顶、超速保 护，行走超限动作', 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '主接触器断开', '2018-11-14 11:14:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('57', 'HorizontalBreakerOrBrakeInterrupt', '水平断路器/制动器跳闸', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '水平断路器/制动器跳闸', '2018-11-14 11:15:09', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('58', 'RaisingBreakerOrBrakeInterrupt', '是否起升断路器/制动器跳闸', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '起升断路器/制动器跳闸', '2018-11-14 11:15:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('59', 'HorizontalLeadingendOut', '是否水平前端超限（前进终点）', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '水平前端超限（前进终点）', '2018-11-14 11:16:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('60', 'HorizontalTrailingendOut', '是否水平后端超限（后退终点）', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '水平后端超限（后退终点）', '2018-11-14 11:17:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('61', 'VerticalHorizontalLeadingendOut', '是否垂直上端超限（上升终点）', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '垂直上端超限（上升终点）', '2018-11-14 11:18:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('62', 'VerticalHorizontalTrailingendOut', '垂直下端超限（下降终点）', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '垂直下端超限（下降终点）', '2018-11-14 11:19:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('63', 'HorizontalUuivertorSpeed', '是否水平变频器速度超过设定值', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无过载', '水平变频器速度超过设定值', '2018-11-14 11:21:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('64', 'RaisingUuivertorSpeed', '是否起升变频器速度超过设定值', '3', null, 'PLC', 'BOOL', null, null, '1', 'False', '无松绳', '起升变频器速度超过设定值', '2018-11-14 11:23:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('65', 'Number', '堆垛机编号', '2', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:11:39', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('66', 'OperationModel', '操作模式', '2', '操作模式：1-维修；2-手动；3-机载操作；4-单机自动；5-联机', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:12:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('67', 'HeartBeat', '心跳', '2', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:13:24', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('68', 'TaskLimit', '任务限制', '2', '任务限制：1-无限制，2-入库限制，3-出库限制', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:14:12', 'admin', '2018-11-14 10:16:13', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('69', 'Fork1TaskExcuteStatus', '货叉1_任务执行', '2', '1-待机；2-任务执行中;3-任务完成；4-任务中断（出错）；5-下发任务错误', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:20:26', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('70', 'Fork1TaskNo', '货叉1_任务号', '2', '任务号', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:21:18', 'admin', '2018-11-23 15:06:22', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('71', 'Fork1TaskType', '货叉1_任务类型', '2', '货叉任务类型：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:23:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('72', 'Fork2TaskExcuteStatus', '货叉2_任务执行', '2', '1-待机；2-任务执行中;3-任务完成；4-任务中断（出错）；5-下发任务错误', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:20:26', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('73', 'Fork2TaskNo', '货叉2_任务号', '2', '任务号', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:21:18', 'admin', '2018-11-23 15:06:29', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('74', 'Fork2TaskType', '货叉2_任务类型', '2', '货叉任务类型：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:23:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('75', 'HorizontalDistance', '水平测距', '2', '水平测距数据 单位 1mm', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:27:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('76', 'VerticalDistance', '起升测距', '2', '起升测距数据 单位 1mm', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:28:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('77', 'Fork1Distance', '货叉1伸叉测距', '2', '货叉1伸叉测距数据 单位 1mm', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:30:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('78', 'CurrentColumn', '当前列', '2', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:30:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('79', 'CurrentLayer', '当前层', '2', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:31:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('80', 'CurrentStation', '当前出/入口', '2', '当前出/入口 1-10', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 10:32:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('81', 'Fork1FrontOut', '货叉1_是否货物前超', '2', '-无超限；1-货物前超', 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '货物前超', '2018-11-14 10:34:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('82', 'Fork1BehindOut', '货叉1_是否货物后超', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '货物后超', '2018-11-14 10:35:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('83', 'Fork1LeftForkOut', '货叉1_是否左侧外形超限', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '左侧外形超限', '2018-11-14 10:37:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('84', 'Fork1RightForkOut', '货叉1_是否右侧外形超限', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '右侧外形超限', '2018-11-14 10:38:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('85', 'Fork1OverHeight1', '货叉1_超高1', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '超高 1（货物高度与送货地址不匹配）', '2018-11-14 10:39:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('86', 'Fork1OverHeight2', '货叉1_超高2', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '超高 2（货物高度与送货地址不匹配）', '2018-11-14 10:39:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('87', 'Fork1OverHeight3', '货叉1_超高3', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '超高 3（货物高度与送货地址不匹配）', '2018-11-14 10:40:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('88', 'Fork1OverHeight', '货叉1_货物超高', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '货物超高', '2018-11-14 10:40:50', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('89', 'Fork1PalletForkTimeout', '货叉1_货叉超时', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超时', '货叉超时', '2018-11-14 10:41:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('90', 'Fork1LeftLimitAlarm', '货叉1_是否左侧极限报警', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '左侧极限报警', '2018-11-14 10:45:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('91', 'Fork1RightLimitAlarm', '货叉1_是否右侧极限报警', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '右侧极限报警', '2018-11-14 10:47:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('92', 'Fork1ForkUuivertor', '货叉1_货叉变频器故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉变频器故障', '2018-11-14 10:48:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('93', 'Fork1ForkBreakerOrCocontactor', '货叉1_货叉断路器/接触器故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉断路器/接触器故障', '2018-11-14 10:49:14', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('94', 'Fork1GoodsInspectionSensor', '货叉1_是否货物检测传感器故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货物检测传感器故障', '2018-11-14 10:51:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('95', 'Fork1ForkAlignmentSensor', '货叉1_是否货叉定位传感器故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉定位传感器故障', '2018-11-14 10:52:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('96', 'Fork1DirectionError', '货叉1_是否运行方向错误', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '运行方向错误', '2018-11-14 10:56:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('97', 'Fork1XYForkExcute', '货叉1_是否货叉执行动作错误', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', 'X轴、Y轴、货叉执行动作错误', '2018-11-14 10:58:48', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('98', 'Fork1SetValueError', '货叉1_是否设定值错误', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '设定值错误', '2018-11-14 10:59:12', 'admin', '2018-11-22 10:06:17', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('99', 'Fork1PickupTaskError', '货叉1_是否取货任务错误', '2', '0-无故障；1-取货任务错误（取左2右2时，左1右1有货）', 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '取货任务错误', '2018-11-14 10:59:28', 'admin', '2018-11-22 10:06:41', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('100', 'Fork1Spare3', '货叉1_Spare3', '2', null, 'PLC', 'BOOL', null, null, '0', null, null, null, '2018-11-14 10:59:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('101', 'Fork1DoubleIn', '货叉1_双重入库', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '双重入库 满入', '2018-11-14 11:00:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('102', 'Fork1EmptyOut', '货叉1_是否空货位出库', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '空货位出库 空出', '2018-11-14 11:01:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('103', 'Fork1ForkHasPallet', '货叉1_是否货叉有货', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '货叉无货', '货叉有货', '2018-11-14 11:03:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('104', 'Fork1ForkError', '货叉1_货叉总故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉故障', '2018-11-14 11:03:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('105', 'Fork1Spare4', '货叉1_Spare4', '2', null, 'PLC', 'BYTE', null, null, '0', null, null, null, '2018-11-14 11:04:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('106', 'Fork2FrontOut', '货叉2_是否货物前超', '2', '-无超限；1-货物前超', 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '货物前超', '2018-11-14 10:34:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('107', 'Fork2BehindOut', '货叉2_是否货物后超', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '货物后超', '2018-11-14 10:35:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('108', 'Fork2LeftForkOut', '货叉2_是否左侧外形超限', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '左侧外形超限', '2018-11-14 10:37:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('109', 'Fork2RightForkOut', '货叉2_是否右侧外形超限', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '右侧外形超限', '2018-11-14 10:38:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('110', 'Fork2OverHeight1', '货叉2_超高1', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '超高 1（货物高度与送货地址不匹配）', '2018-11-14 10:39:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('111', 'Fork2OverHeight2', '货叉2_超高2', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '超高 2（货物高度与送货地址不匹配）', '2018-11-14 10:39:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('112', 'Fork2OverHeight3', '货叉2_超高3', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '超高 3（货物高度与送货地址不匹配）', '2018-11-14 10:40:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('113', 'Fork2OverHeight', '货叉2_货物超高', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超高', '货物超高', '2018-11-14 10:40:50', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('114', 'Fork2PalletForkTimeout', '货叉2_货叉超时', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超时', '货叉超时', '2018-11-14 10:41:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('115', 'Fork2LeftLimitAlarm', '货叉2_是否左侧极限报警', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '左侧极限报警', '2018-11-14 10:45:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('116', 'Fork2RightLimitAlarm', '货叉2_是否右侧极限报警', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '右侧极限报警', '2018-11-14 10:47:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('117', 'Fork2ForkUuivertor', '货叉2_货叉变频器故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉变频器故障', '2018-11-14 10:48:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('118', 'Fork2ForkBreakerOrCocontactor', '货叉2_货叉断路器/接触器故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉断路器/接触器故障', '2018-11-14 10:49:14', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('119', 'Fork2GoodsInspectionSensor', '货叉2_是否货物检测传感器故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货物检测传感器故障', '2018-11-14 10:51:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('120', 'Fork2ForkAlignmentSensor', '货叉2_是否货叉定位传感器故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉定位传感器故障', '2018-11-14 10:52:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('121', 'Fork2DirectionError', '货叉2_是否运行方向错误', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '运行方向错误', '2018-11-14 10:56:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('122', 'Fork2XYForkExcute', '货叉2_是否货叉执行动作错误', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', 'X轴、Y轴、货叉执行动作错误', '2018-11-14 10:58:48', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('123', 'Fork2SetValueError', '货叉2_是否设定值错误', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '设定值错误', '2018-11-14 10:59:12', 'admin', '2018-11-22 10:07:38', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('124', 'Fork2PickupTaskError', '货叉2_是否取货任务错误', '2', '0-无故障；1-取货任务错误（取左2右2时，左1右1有货）', 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '取货任务错误', '2018-11-14 10:59:28', 'admin', '2018-11-22 10:08:18', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('125', 'Fork2Spare3', '货叉2_Spare3', '2', null, 'PLC', 'BOOL', null, null, '0', null, null, null, '2018-11-14 10:59:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('126', 'Fork2DoubleIn', '货叉2_双重入库', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '双重入库 满入', '2018-11-14 11:00:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('127', 'Fork2EmptyOut', '货叉2_是否空货位出库', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '空货位出库 空出', '2018-11-14 11:01:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('128', 'Fork2ForkHasPallet', '货叉2_是否货叉有货', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '货叉无货', '货叉有货', '2018-11-14 11:03:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('129', 'Fork2ForkError', '货叉2_货叉总故障', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '货叉故障', '2018-11-14 11:03:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('130', 'Fork2Spare4', '货叉2_Spare4', '2', null, 'PLC', 'BYTE', null, null, '0', null, null, null, '2018-11-14 11:04:18', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('131', 'Overload', '是否过载', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无过载', '堆垛机故障', '2018-11-14 11:07:32', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('132', 'Rope', '是否松绳', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无松绳', '松绳', '2018-11-14 11:08:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('133', 'RunningUuivertorAlarm', '是否行走变频器报警', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无报警', '行走变频器报警', '2018-11-14 11:08:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('134', 'RaisingUuivertorAlarm', '是否起升变频器报警', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无报警', '起升变频器报警', '2018-11-14 11:09:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('135', 'RunningTimeout', '是否运行超时', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超时', '运行超时', '2018-11-14 11:10:10', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('136', 'RaisingTimeout', '是否起升超时', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超时', '起升超时', '2018-11-14 11:10:45', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('137', 'HorizontalLaserDataError', '是否水平激光数据错误', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无错误', '水平激光数据错误', '2018-11-14 11:11:23', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('138', 'RaisingBarcodeDataError', '起升条码数据错误', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无错误', '起升条码数据错误', '2018-11-14 11:11:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('139', 'AdressError', '是否地址错', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '地址错', '2018-11-14 11:12:40', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('140', 'MainCocontactorInterrupt', '主接触器断开', '2', '急停、冲 顶、超速保 护，行走超限动作', 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '主接触器断开', '2018-11-14 11:14:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('141', 'HorizontalBreakerOrBrakeInterrupt', '水平断路器/制动器跳闸', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '水平断路器/制动器跳闸', '2018-11-14 11:15:09', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('142', 'RaisingBreakerOrBrakeInterrupt', '是否起升断路器/制动器跳闸', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无故障', '起升断路器/制动器跳闸', '2018-11-14 11:15:55', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('143', 'HorizontalLeadingendOut', '是否水平前端超限（前进终点）', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '水平前端超限（前进终点）', '2018-11-14 11:16:49', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('144', 'HorizontalTrailingendOut', '是否水平后端超限（后退终点）', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '水平后端超限（后退终点）', '2018-11-14 11:17:35', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('145', 'VerticalHorizontalLeadingendOut', '是否垂直上端超限（上升终点）', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '垂直上端超限（上升终点）', '2018-11-14 11:18:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('146', 'VerticalHorizontalTrailingendOut', '垂直下端超限（下降终点）', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无超限', '垂直下端超限（下降终点）', '2018-11-14 11:19:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('147', 'HorizontalUuivertorSpeed', '是否水平变频器速度超过设定值', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无过载', '水平变频器速度超过设定值', '2018-11-14 11:21:47', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('148', 'RaisingUuivertorSpeed', '是否起升变频器速度超过设定值', '2', null, 'PLC', 'BOOL', null, null, '1', 'False', '无松绳', '起升变频器速度超过设定值', '2018-11-14 11:23:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('149', 'WCSForkAction', '货叉动作类型', '3', '0=无，1=1号货叉，2=2号货叉，3=同时动作', 'PLC', 'BOOL', null, null, '0', null, null, null, '2018-11-14 11:45:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('150', 'WCSFork1TaskFlag', '货叉1_任务标志', '3', '任务标志：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址, 6删除任务,10任务完成；任务完成时清除（由WCS清空为0）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:46:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('151', 'WCSFork1Row', '货叉1_取放货地址:  行', '3', '取放货地址: 行  (1=左1，2=左2，3=右1，4=右2)', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:52:12', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('152', 'WCSFork1Column', '货叉1_取放货列', '3', '列（1-最远列）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:53:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('153', 'WCSFork1Layer', '货叉1_取放货层', '3', '取放货地址: 层（1-最高层）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:54:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('154', 'WCSFork1Station', '货叉1_取放货出入口', '3', '取放货出入口（1-10）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:55:02', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('155', 'WCSFork1Task', '货叉1_任务号', '3', '货叉1任务号', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 11:55:47', 'admin', '2018-11-23 15:06:09', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('156', 'WCSHeartBeat', '心跳', '3', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:56:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('157', 'WCSForkAction', '货叉动作类型', '2', '0=无，1=1号货叉，2=2号货叉，3=同时动作', 'PLC', 'BOOL', null, null, '0', null, null, null, '2018-11-14 11:45:08', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('158', 'WCSFork1TaskFlag', '货叉1_任务标志', '2', '任务标志：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址, 6删除任务,10任务完成；任务完成时清除（由WCS清空为0）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:46:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('159', 'WCSFork1Row', '货叉1_取放货地址:  行', '2', '取放货地址: 行  (1=左1，2=左2，3=右1，4=右2)', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:52:12', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('160', 'WCSFork1Column', '货叉1_取放货列', '2', '列（1-最远列）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:53:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('161', 'WCSFork1Layer', '货叉1_取放货层', '2', '取放货地址: 层（1-最高层）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:54:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('162', 'WCSFork1Station', '货叉1_取放货出入口', '2', '取放货出入口（1-10）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:55:02', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('163', 'WCSFork1Task', '货叉1_任务号', '2', '货叉1任务号', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 11:55:47', 'admin', '2018-11-23 15:06:39', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('164', 'WCSFork2TaskFlag', '货叉2_任务标志', '2', '任务标志：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址, 6删除任务,10任务完成；任务完成时清除（由WCS清空为0）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:46:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('165', 'WCSFork2Row', '货叉2_取放货地址:  行', '2', '取放货地址: 行  (1=左1，2=左2，3=右1，4=右2)', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:52:12', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('166', 'WCSFork2Column', '货叉2_取放货列', '2', '列（1-最远列）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:53:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('167', 'WCSFork2Layer', '货叉2_取放货层', '2', '取放货地址: 层（1-最高层）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:54:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('168', 'WCSFork2Station', '货叉2_取放货出入口', '2', '取放货出入口（1-10）', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:55:02', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('169', 'WCSFork2Task', '货叉2_任务号', '2', '货叉1任务号', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 11:55:47', 'admin', '2018-11-23 15:06:45', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('170', 'WCSHeartBeat', '心跳', '2', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 11:56:13', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('171', 'RequestMessage', '地址请求', '4', '地址请求01', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('172', 'RequestLoadStatus', '地址请求-装载状态', '4', '2B', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('173', 'RequestNumber', '地址请求-读码器编号', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('174', 'RequestBarcode', '地址请求-条码', '4', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('175', 'RequestWeight', '地址请求-货物重量', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('176', 'RequestLength', '地址请求-货物长度', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('177', 'RequestWidth', '地址请求-货物宽度', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('178', 'RequestHeight', '地址请求-货物高度', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('179', 'RequestRetCode', '地址请求-RetCode', '4', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('180', 'RequestBackup', '地址请求-备用', '4', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('181', 'ArriveMessage', '位置到达-报文', '4', '位置到达 02', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2018-11-14 13:58:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('182', 'ArriveResult', '位置到达-结果', '4', '1成功，2失败', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('183', 'ArriveRealAddress', '位置到达-实际到达地址', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('184', 'ArriveAllocationAddress', '位置到达-WCS分配地址', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', '2019-09-02 16:19:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('185', 'ArriveBarcode', '位置到达-条码', '4', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('186', 'ArrivePaddingBit', '位置到达-填充位', '4', '填充0', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('187', 'ControlMessage', '控制指令-报文', '4', '控制指令PLC-WCS 03', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:53:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('188', 'ControlType', '控制指令-类型', '4', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:55:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('189', 'ControlNumber', '控制指令-站台编号', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:56:03', 'admin', '2018-11-14 13:56:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('190', 'ControlBackup', '控制指令-备用', '4', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:56:41', 'admin', '2019-09-02 16:38:44', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('192', 'ACKMessage', 'ACK报文', '4', '05 PLC->WCS', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('193', 'ACKLoadStatus', 'ACK装载状态', '4', '回复WCS控制指令时，置2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:58', 'admin', '2018-11-23 16:32:27', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('194', 'ACKNumber', 'ACK读码器编号', '4', '这个就是站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:00:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('195', 'ACKBackup', 'ACK备用', '4', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:00:46', 'admin', '2019-09-02 16:38:59', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('196', 'WCSReplyMessage', 'WCS地址回复报文', '4', '06 地址回复', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('197', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('198', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2018-11-14 14:04:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('199', 'WCSReplyBarcode', 'WCS地址回复-条码', '4', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('200', 'WCSReplyWeight', 'WCS地址回复-货物重量', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('201', 'WCSReplyLength', 'WCS地址回复-货物长度', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('202', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('203', 'WCSReplyHeight', 'WCS地址回复-货物高度', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('204', 'WCSReplyAddress', 'WCS地址回复-目标地址', '4', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('205', 'WCSReplyBackup', 'WCS地址回复-备用', '4', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', '2019-09-02 16:39:24', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('206', 'WCSControlMessage', 'WCS控制指令-报文', '4', '07 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('207', 'WCSControlType', 'WCS控制指令-报文类型', '4', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('208', 'WCSControlNumber', 'WCS控制指令-读码器编号', '4', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('209', 'WCSControlBackup', 'WCS控制指令-备用', '4', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('210', 'WCSACKMessage', 'WCSACK报文', '4', '08 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('211', 'WCSACKLoadStatus', 'WCSACK-装载状态', '4', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('212', 'WCSACKNumber', 'WCSACK-读码器编码', '4', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2018-11-14 14:17:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('213', 'WCSACKBackup', 'WCSACK-备用', '4', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('234', 'RequestMessage', '地址请求', '5', '地址请求01', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('235', 'RequestLoadStatus', '地址请求-装载状态', '5', '2B', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('236', 'RequestNumber', '地址请求-读码器编号', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('237', 'RequestBarcode', '地址请求-条码', '5', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('238', 'RequestWeight', '地址请求-货物重量', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('239', 'RequestLength', '地址请求-货物长度', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('240', 'RequestWidth', '地址请求-货物宽度', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('241', 'RequestHeight', '地址请求-货物高度', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('242', 'RequestRetCode', '地址请求-RetCode', '5', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('243', 'RequestBackup', '地址请求-备用', '5', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('244', 'ArriveMessage', '位置到达-报文', '5', '位置到达 02', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2018-11-14 13:58:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('245', 'ArriveResult', '位置到达-结果', '5', '1成功，2失败', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('246', 'ArriveRealAddress', '位置到达-实际到达地址', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('247', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('248', 'ArriveBarcode', '位置到达-条码', '5', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('249', 'ArrivePaddingBit', '位置到达-填充位', '5', '填充0', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('250', 'ControlMessage', '控制指令-报文', '5', '控制指令PLC-WCS 03', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:53:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('251', 'ControlType', '控制指令-类型', '5', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:55:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('252', 'ControlNumber', '控制指令-站台编号', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:56:03', 'admin', '2018-11-14 13:56:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('253', 'ControlBackUp', '控制指令-备用', '5', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:56:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('254', 'ACKMessage', 'ACK报文', '5', '05 PLC->WCS', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('255', 'ACKLoadStatus', 'ACK装载状态', '5', '回复WCS控制指令时，置2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:58', 'admin', '2018-11-23 16:32:41', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('256', 'ACKNumber', 'ACK读码器编号', '5', '这个就是站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:00:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('257', 'ACKBackUp', 'ACK备用', '5', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:00:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('258', 'WCSReplyMessage', 'WCS地址回复报文', '5', '06 地址回复', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('259', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('260', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2018-11-14 14:04:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('261', 'WCSReplyBarcode', 'WCS地址回复-条码', '5', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('262', 'WCSReplyWeight', 'WCS地址回复-货物重量', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('263', 'WCSReplyLength', 'WCS地址回复-货物长度', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('264', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('265', 'WCSReplyHeight', 'WCS地址回复-货物高度', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('266', 'WCSReplyAddress', 'WCS地址回复-目标地址', '5', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('267', 'WCSReplyBackUp', 'WCS地址回复-备用', '5', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('268', 'WCSControlMessage', 'WCS控制指令-报文', '5', '07 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('269', 'WCSControlType', 'WCS控制指令-报文类型', '5', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('270', 'WCSControlNumber', 'WCS控制指令-读码器编号', '5', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('271', 'WCSControlBackup', 'WCS控制指令-备用', '5', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('272', 'WCSACKMessage', 'WCSACK报文', '5', '08 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('273', 'WCSACKLoadStatus', 'WCSACK-装载状态', '5', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('274', 'WCSACKNumber', 'WCSACK-读码器编码', '5', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2018-11-14 14:17:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('275', 'WCSACKBackup', 'WCSACK-备用', '5', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('276', 'RequestMessage', '地址请求', '7', '地址请求01', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('277', 'RequestLoadStatus', '地址请求-装载状态', '7', '2B', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('278', 'RequestNumber', '地址请求-读码器编号', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('279', 'RequestBarcode', '地址请求-条码', '7', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('280', 'RequestWeight', '地址请求-货物重量', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('281', 'RequestLength', '地址请求-货物长度', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('282', 'RequestWidth', '地址请求-货物宽度', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('283', 'RequestHeight', '地址请求-货物高度', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('284', 'RequestRetCode', '地址请求-RetCode', '7', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('285', 'RequestBackup', '地址请求-备用', '7', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('286', 'ArriveMessage', '位置到达-报文', '7', '位置到达 02', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2018-11-14 13:58:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('287', 'ArriveResult', '位置到达-结果', '7', '1成功，2失败', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('288', 'ArriveRealAddress', '位置到达-实际到达地址', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('289', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('290', 'ArriveBarcode', '位置到达-条码', '7', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('291', 'ArrivePaddingBit', '位置到达-填充位', '7', '填充0', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('292', 'ControlMessage', '控制指令-报文', '7', '控制指令PLC-WCS 03', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:53:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('293', 'ControlType', '控制指令-类型', '7', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:55:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('294', 'ControlNumber', '控制指令-站台编号', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:56:03', 'admin', '2018-11-14 13:56:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('295', 'ControlBackUp', '控制指令-备用', '7', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:56:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('296', 'ACKMessage', 'ACK报文', '7', '05 PLC->WCS', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('297', 'ACKLoadStatus', 'ACK装载状态', '7', '回复WCS控制指令时，置2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:58', 'admin', '2018-11-23 16:32:59', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('298', 'ACKNumber', 'ACK读码器编号', '7', '这个就是站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:00:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('299', 'ACKBackUp', 'ACK备用', '7', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:00:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('300', 'WCSReplyMessage', 'WCS地址回复报文', '7', '06 地址回复', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('301', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('302', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2018-11-14 14:04:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('303', 'WCSReplyBarcode', 'WCS地址回复-条码', '7', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('304', 'WCSReplyWeight', 'WCS地址回复-货物重量', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('305', 'WCSReplyLength', 'WCS地址回复-货物长度', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('306', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('307', 'WCSReplyHeight', 'WCS地址回复-货物高度', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('308', 'WCSReplyAddress', 'WCS地址回复-目标地址', '7', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('309', 'WCSReplyBackUp', 'WCS地址回复-备用', '7', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('310', 'WCSControlMessage', 'WCS控制指令-报文', '7', '07 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('311', 'WCSControlType', 'WCS控制指令-报文类型', '7', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('312', 'WCSControlNumber', 'WCS控制指令-读码器编号', '7', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('313', 'WCSControlBackup', 'WCS控制指令-备用', '7', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('314', 'WCSACKMessage', 'WCSACK报文', '7', '08 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('315', 'WCSACKLoadStatus', 'WCSACK-装载状态', '7', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('316', 'WCSACKNumber', 'WCSACK-读码器编码', '7', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2018-11-14 14:17:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('317', 'WCSACKBackup', 'WCSACK-备用', '7', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('324', 'Fork2Distance', '货叉2伸叉测距', '2', '货叉2伸叉测距数据 单位 1mm', 'PLC', 'DINT', null, null, '0', null, null, null, '2018-11-14 10:30:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('325', 'RequestMessage', '地址请求', '8', '地址请求01', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('326', 'RequestLoadStatus', '地址请求-装载状态', '8', '2B', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('327', 'RequestNumber', '地址请求-读码器编号', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('328', 'RequestBarcode', '地址请求-条码', '8', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('329', 'RequestWeight', '地址请求-货物重量', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('330', 'RequestLength', '地址请求-货物长度', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('331', 'RequestWidth', '地址请求-货物宽度', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('332', 'RequestHeight', '地址请求-货物高度', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('333', 'RequestRetCode', '地址请求-RetCode', '8', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('334', 'RequestBackup', '地址请求-备用', '8', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('335', 'ArriveMessage', '位置到达-报文', '8', '位置到达 02', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2018-11-14 13:58:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('336', 'ArriveResult', '位置到达-结果', '8', '1成功，2失败', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('337', 'ArriveRealAddress', '位置到达-实际到达地址', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('338', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('339', 'ArriveBarcode', '位置到达-条码', '8', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('340', 'ArrivePaddingBit', '位置到达-填充位', '8', '填充0', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('341', 'ControlMessage', '控制指令-报文', '8', '控制指令PLC-WCS 03', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:53:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('342', 'ControlType', '控制指令-类型', '8', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:55:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('343', 'ControlNumber', '控制指令-站台编号', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:56:03', 'admin', '2018-11-14 13:56:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('344', 'ControlBackUp', '控制指令-备用', '8', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:56:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('345', 'ACKMessage', 'ACK报文', '8', '05 PLC->WCS', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('346', 'ACKLoadStatus', 'ACK装载状态', '8', '回复WCS控制指令时，置2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:58', 'admin', '2018-11-23 16:33:15', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('347', 'ACKNumber', 'ACK读码器编号', '8', '这个就是站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:00:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('348', 'ACKBackUp', 'ACK备用', '8', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:00:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('349', 'WCSReplyMessage', 'WCS地址回复报文', '8', '06 地址回复', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('350', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('351', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2018-11-14 14:04:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('352', 'WCSReplyBarcode', 'WCS地址回复-条码', '8', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('353', 'WCSReplyWeight', 'WCS地址回复-货物重量', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('354', 'WCSReplyLength', 'WCS地址回复-货物长度', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('355', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('356', 'WCSReplyHeight', 'WCS地址回复-货物高度', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('357', 'WCSReplyAddress', 'WCS地址回复-目标地址', '8', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('358', 'WCSReplyBackUp', 'WCS地址回复-备用', '8', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('359', 'WCSControlMessage', 'WCS控制指令-报文', '8', '07 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('360', 'WCSControlType', 'WCS控制指令-报文类型', '8', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('361', 'WCSControlNumber', 'WCS控制指令-读码器编号', '8', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('362', 'WCSControlBackup', 'WCS控制指令-备用', '8', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('363', 'WCSACKMessage', 'WCSACK报文', '8', '08 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('364', 'WCSACKLoadStatus', 'WCSACK-装载状态', '8', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('365', 'WCSACKNumber', 'WCSACK-读码器编码', '8', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2018-11-14 14:17:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('366', 'WCSACKBackup', 'WCSACK-备用', '8', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('367', 'RequestMessage', '地址请求', '9', '地址请求01', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('368', 'RequestLoadStatus', '地址请求-装载状态', '9', '2B', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('369', 'RequestNumber', '地址请求-读码器编号', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('370', 'RequestBarcode', '地址请求-条码', '9', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('371', 'RequestWeight', '地址请求-货物重量', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('372', 'RequestLength', '地址请求-货物长度', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('373', 'RequestWidth', '地址请求-货物宽度', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('374', 'RequestHeight', '地址请求-货物高度', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('375', 'RequestRetCode', '地址请求-RetCode', '9', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('376', 'RequestBackup', '地址请求-备用', '9', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('377', 'ArriveMessage', '位置到达-报文', '9', '位置到达 02', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2018-11-14 13:58:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('378', 'ArriveResult', '位置到达-结果', '9', '1成功，2失败', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('379', 'ArriveRealAddress', '位置到达-实际到达地址', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('380', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('381', 'ArriveBarcode', '位置到达-条码', '9', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('382', 'ArrivePaddingBit', '位置到达-填充位', '9', '填充0', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('383', 'ControlMessage', '控制指令-报文', '9', '控制指令PLC-WCS 03', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:53:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('384', 'ControlType', '控制指令-类型', '9', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:55:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('385', 'ControlNumber', '控制指令-站台编号', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:56:03', 'admin', '2018-11-14 13:56:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('386', 'ControlBackUp', '控制指令-备用', '9', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:56:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('387', 'ACKMessage', 'ACK报文', '9', '05 PLC->WCS', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('388', 'ACKLoadStatus', 'ACK装载状态', '9', '回复WCS控制指令时，置2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:58', 'admin', '2018-11-23 16:33:29', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('389', 'ACKNumber', 'ACK读码器编号', '9', '这个就是站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:00:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('390', 'ACKBackUp', 'ACK备用', '9', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:00:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('391', 'WCSReplyMessage', 'WCS地址回复报文', '9', '06 地址回复', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('392', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('393', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2018-11-14 14:04:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('394', 'WCSReplyBarcode', 'WCS地址回复-条码', '9', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('395', 'WCSReplyWeight', 'WCS地址回复-货物重量', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('396', 'WCSReplyLength', 'WCS地址回复-货物长度', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('397', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('398', 'WCSReplyHeight', 'WCS地址回复-货物高度', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('399', 'WCSReplyAddress', 'WCS地址回复-目标地址', '9', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('400', 'WCSReplyBackUp', 'WCS地址回复-备用', '9', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('401', 'WCSControlMessage', 'WCS控制指令-报文', '9', '07 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('402', 'WCSControlType', 'WCS控制指令-报文类型', '9', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('403', 'WCSControlNumber', 'WCS控制指令-读码器编号', '9', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('404', 'WCSControlBackup', 'WCS控制指令-备用', '9', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('405', 'WCSACKMessage', 'WCSACK报文', '9', '08 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('406', 'WCSACKLoadStatus', 'WCSACK-装载状态', '9', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('407', 'WCSACKNumber', 'WCSACK-读码器编码', '9', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2018-11-14 14:17:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('408', 'WCSACKBackup', 'WCSACK-备用', '9', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('409', 'RequestMessage', '地址请求', '10', '地址请求01', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('410', 'RequestLoadStatus', '地址请求-装载状态', '10', '2B', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('411', 'RequestNumber', '地址请求-读码器编号', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('412', 'RequestBarcode', '地址请求-条码', '10', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('413', 'RequestWeight', '地址请求-货物重量', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('414', 'RequestLength', '地址请求-货物长度', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('415', 'RequestWidth', '地址请求-货物宽度', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('416', 'RequestHeight', '地址请求-货物高度', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('417', 'RequestRetCode', '地址请求-RetCode', '10', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('418', 'RequestBackup', '地址请求-备用', '10', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('419', 'ArriveMessage', '位置到达-报文', '10', '位置到达 02', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2018-11-14 13:58:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('420', 'ArriveResult', '位置到达-结果', '10', '1成功，2失败', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('421', 'ArriveRealAddress', '位置到达-实际到达地址', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('422', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('423', 'ArriveBarcode', '位置到达-条码', '10', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('424', 'ArrivePaddingBit', '位置到达-填充位', '10', '填充0', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('425', 'ControlMessage', '控制指令-报文', '10', '控制指令PLC-WCS 03', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:53:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('426', 'ControlType', '控制指令-类型', '10', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:55:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('427', 'ControlNumber', '控制指令-站台编号', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:56:03', 'admin', '2018-11-14 13:56:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('428', 'ControlBackUp', '控制指令-备用', '10', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:56:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('429', 'ACKMessage', 'ACK报文', '10', '05 PLC->WCS', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('430', 'ACKLoadStatus', 'ACK装载状态', '10', '回复WCS控制指令时，置2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:58', 'admin', '2018-11-23 16:31:39', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('431', 'ACKNumber', 'ACK读码器编号', '10', '这个就是站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:00:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('432', 'ACKBackUp', 'ACK备用', '10', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:00:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('433', 'WCSReplyMessage', 'WCS地址回复报文', '10', '06 地址回复', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('434', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('435', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2018-11-14 14:04:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('436', 'WCSReplyBarcode', 'WCS地址回复-条码', '10', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('437', 'WCSReplyWeight', 'WCS地址回复-货物重量', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('438', 'WCSReplyLength', 'WCS地址回复-货物长度', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('439', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('440', 'WCSReplyHeight', 'WCS地址回复-货物高度', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('441', 'WCSReplyAddress', 'WCS地址回复-目标地址', '10', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('442', 'WCSReplyBackUp', 'WCS地址回复-备用', '10', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('443', 'WCSControlMessage', 'WCS控制指令-报文', '10', '07 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('444', 'WCSControlType', 'WCS控制指令-报文类型', '10', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('445', 'WCSControlNumber', 'WCS控制指令-读码器编号', '10', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('446', 'WCSControlBackup', 'WCS控制指令-备用', '10', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('447', 'WCSACKMessage', 'WCSACK报文', '10', '08 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('448', 'WCSACKLoadStatus', 'WCSACK-装载状态', '10', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('449', 'WCSACKNumber', 'WCSACK-读码器编码', '10', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2018-11-14 14:17:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('450', 'WCSACKBackup', 'WCSACK-备用', '10', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('451', 'RequestMessage', '地址请求', '11', '地址请求01', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('452', 'RequestLoadStatus', '地址请求-装载状态', '11', '2B', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('453', 'RequestNumber', '地址请求-读码器编号', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('454', 'RequestBarcode', '地址请求-条码', '11', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('455', 'RequestWeight', '地址请求-货物重量', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('456', 'RequestLength', '地址请求-货物长度', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('457', 'RequestWidth', '地址请求-货物宽度', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('458', 'RequestHeight', '地址请求-货物高度', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('459', 'RequestRetCode', '地址请求-RetCode', '11', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('460', 'RequestBackup', '地址请求-备用', '11', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('461', 'ArriveMessage', '位置到达-报文', '11', '位置到达 02', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2018-11-14 13:58:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('462', 'ArriveResult', '位置到达-结果', '11', '1成功，2失败', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('463', 'ArriveRealAddress', '位置到达-实际到达地址', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('464', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('465', 'ArriveBarcode', '位置到达-条码', '11', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('466', 'ArrivePaddingBit', '位置到达-填充位', '11', '填充0', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('467', 'ControlMessage', '控制指令-报文', '11', '控制指令PLC-WCS 03', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:53:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('468', 'ControlType', '控制指令-类型', '11', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:55:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('469', 'ControlNumber', '控制指令-站台编号', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:56:03', 'admin', '2018-11-14 13:56:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('470', 'ControlBackUp', '控制指令-备用', '11', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:56:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('471', 'ACKMessage', 'ACK报文', '11', '05 PLC->WCS', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('472', 'ACKLoadStatus', 'ACK装载状态', '11', '回复WCS控制指令时，置2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:58', 'admin', '2018-11-23 16:32:27', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('473', 'ACKNumber', 'ACK读码器编号', '11', '这个就是站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:00:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('474', 'ACKBackUp', 'ACK备用', '11', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:00:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('475', 'WCSReplyMessage', 'WCS地址回复报文', '11', '06 地址回复', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('476', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('477', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2018-11-14 14:04:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('478', 'WCSReplyBarcode', 'WCS地址回复-条码', '11', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('479', 'WCSReplyWeight', 'WCS地址回复-货物重量', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('480', 'WCSReplyLength', 'WCS地址回复-货物长度', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('481', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('482', 'WCSReplyHeight', 'WCS地址回复-货物高度', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('483', 'WCSReplyAddress', 'WCS地址回复-目标地址', '11', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('484', 'WCSReplyBackUp', 'WCS地址回复-备用', '11', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('485', 'WCSControlMessage', 'WCS控制指令-报文', '11', '07 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('486', 'WCSControlType', 'WCS控制指令-报文类型', '11', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('487', 'WCSControlNumber', 'WCS控制指令-读码器编号', '11', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('488', 'WCSControlBackup', 'WCS控制指令-备用', '11', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('489', 'WCSACKMessage', 'WCSACK报文', '11', '08 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('490', 'WCSACKLoadStatus', 'WCSACK-装载状态', '11', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('491', 'WCSACKNumber', 'WCSACK-读码器编码', '11', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2018-11-14 14:17:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('492', 'WCSACKBackup', 'WCSACK-备用', '11', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('493', 'WCSCanOut', '是否允许开关切换（WCS-->PLC)', '3', '1允许切换,0不允许切换', 'PLC', 'BOOL', null, null, '0', null, null, null, '2018-11-26 13:24:36', 'liufu', '2018-11-26 13:28:00', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('494', 'SwitchStatus', '切换开关状态', '3', null, 'PLC', 'BOOL', null, null, '0', null, null, null, '2018-11-26 13:25:30', 'liufu', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('537', 'RequestMessage', '地址请求', '13', '地址请求01', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:41:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('538', 'RequestLoadStatus', '地址请求-装载状态', '13', '2B', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:42:16', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('539', 'RequestNumber', '地址请求-读码器编号', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:44:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('540', 'RequestBarcode', '地址请求-条码', '13', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:45:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('541', 'RequestWeight', '地址请求-货物重量', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:45:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('542', 'RequestLength', '地址请求-货物长度', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('543', 'RequestWidth', '地址请求-货物宽度', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:46:53', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('544', 'RequestHeight', '地址请求-货物高度', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:47:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('545', 'RequestRetCode', '地址请求-RetCode', '13', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:47:58', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('546', 'RequestBackup', '地址请求-备用', '13', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:48:28', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('547', 'ArriveMessage', '位置到达-报文', '13', '位置到达 02', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:49:42', 'admin', '2018-11-14 13:58:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('548', 'ArriveResult', '位置到达-结果', '13', '1成功，2失败', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('549', 'ArriveRealAddress', '位置到达-实际到达地址', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:50:38', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('550', 'ArriveAllcationAddress', '位置到达-WCS分配地址', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:51:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('551', 'ArriveBarcode', '位置到达-条码', '13', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:51:56', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('552', 'ArrivePaddingBit', '位置到达-填充位', '13', '填充0', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:52:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('553', 'ControlMessage', '控制指令-报文', '13', '控制指令PLC-WCS 03', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:53:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('554', 'ControlType', '控制指令-类型', '13', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:55:30', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('555', 'ControlNumber', '控制指令-站台编号', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:56:03', 'admin', '2018-11-14 13:56:10', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('556', 'ControlBackUp', '控制指令-备用', '13', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 13:56:41', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('557', 'ACKMessage', 'ACK报文', '13', '05 PLC->WCS', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('558', 'ACKLoadStatus', 'ACK装载状态', '13', '回复WCS控制指令时，置2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 13:59:58', 'admin', '2018-11-23 16:32:27', 'liufu');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('559', 'ACKNumber', 'ACK读码器编号', '13', '这个就是站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:00:25', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('560', 'ACKBackUp', 'ACK备用', '13', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:00:46', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('561', 'WCSReplyMessage', 'WCS地址回复报文', '13', '06 地址回复', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:01:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('562', 'WCSReplyLoadStatus', 'WCS地址回复-装载状态', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:02:27', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('563', 'WCSReplyNumber', 'WCS地址回复-读码器编码', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:04:17', 'admin', '2018-11-14 14:04:30', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('564', 'WCSReplyBarcode', 'WCS地址回复-条码', '13', 'PLC上报的条码信息', 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:05:00', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('565', 'WCSReplyWeight', 'WCS地址回复-货物重量', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:29', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('566', 'WCSReplyLength', 'WCS地址回复-货物长度', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:05:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('567', 'WCSReplyWidth', 'WCS地址回复-货物宽度', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:19', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('568', 'WCSReplyHeight', 'WCS地址回复-货物高度', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:06:43', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('569', 'WCSReplyAddress', 'WCS地址回复-目标地址', '13', null, 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:07:11', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('570', 'WCSReplyBackUp', 'WCS地址回复-备用', '13', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:07:33', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('571', 'WCSControlMessage', 'WCS控制指令-报文', '13', '07 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:09:52', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('572', 'WCSControlType', 'WCS控制指令-报文类型', '13', '1空托盘组]补给；11周转箱组补给；3站台启用关闭；6拼托完成信号；12托盘码盘完毕；7料箱码完；', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:03', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('573', 'WCSControlNumber', 'WCS控制指令-读码器编号', '13', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:11:37', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('574', 'WCSControlBackup', 'WCS控制指令-备用', '13', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:12:44', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('575', 'WCSACKMessage', 'WCSACK报文', '13', '08 WCS-->PLC', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:21', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('576', 'WCSACKLoadStatus', 'WCSACK-装载状态', '13', '如同一地址发送到达与控制指令时，回复达到为1，回复控制指令为2', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:14:54', 'admin', null, null);
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('577', 'WCSACKNumber', 'WCSACK-读码器编码', '13', '站台编号', 'PLC', 'INT', null, null, '0', null, null, null, '2018-11-14 14:17:04', 'admin', '2018-11-14 14:17:07', 'admin');
INSERT INTO `wcsequipmenttypeproptemplate2` VALUES ('578', 'WCSACKBackup', 'WCSACK-备用', '13', null, 'PLC', 'CHAR', null, null, '0', null, null, null, '2018-11-14 14:18:51', 'admin', null, null);

-- ----------------------------
-- Table structure for wcsinterfacelog
-- ----------------------------
DROP TABLE IF EXISTS `wcsinterfacelog`;
CREATE TABLE `wcsinterfacelog` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `interfaceName` varchar(255) DEFAULT NULL,
  `request` varchar(5000) DEFAULT NULL,
  `response` varchar(5000) DEFAULT NULL,
  `flag` varchar(255) DEFAULT NULL,
  `content` varchar(500) DEFAULT NULL,
  `remark` varchar(500) DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(100) DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of wcsinterfacelog
-- ----------------------------
INSERT INTO `wcsinterfacelog` VALUES ('1', 'TaskCreate', '[{\n	\"taskNo\":3\n}]', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,容器条码出现为空\"}', 'BadRequest', '', '', null, null, null, null);
INSERT INTO `wcsinterfacelog` VALUES ('2', 'TaskAssign', '{\r\n	\"taskNo\": \"201910180001\",\r\n    \"taskType\": \"100\", \r\n    \"containerCode\": \"M00001\",\r\n	\"fromPort\": \"1001\",\r\n	\"toPort\": \"1001\",\r\n    \"fromLocationCode\": \"L010203\",\r\n    \"toLocationCode\": \"L010203\",\r\n	\"priority\": \"100\",\r\n	\"remark\": \"0\",\r\n	\"platform\": \"wms\",\r\n	\"taskDetails\": [{\r\n		\"referLineNo\": \"001\",\r\n        \"materialCode\": \"001201\",\r\n		\"materialName\": \"物料名称\",\r\n		\"qty\": \"0.0\",\r\n		\"unit\": \"PCS\"\r\n	}\r\n	]\r\n}\r\n', '{\"Code\":400,\"Data\":null,\"Msg\":\"创建任务的时候出现异常值不能为 null。\\r\\n参数名: path\"}', 'Error', '', '', '2019-10-18 10:54:30', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('3', 'TaskAssign', '{\r\n	\"taskNo\": \"201910180001\",\r\n    \"taskType\": \"100\", \r\n    \"containerCode\": \"M00001\",\r\n	\"fromPort\": \"1001\",\r\n	\"toPort\": \"1001\",\r\n    \"fromLocationCode\": \"L010203\",\r\n    \"toLocationCode\": \"L010203\",\r\n	\"priority\": \"100\",\r\n	\"remark\": \"0\",\r\n	\"platform\": \"wms\",\r\n	\"taskDetails\": [{\r\n		\"referLineNo\": \"001\",\r\n        \"materialCode\": \"001201\",\r\n		\"materialName\": \"物料名称\",\r\n		\"qty\": \"0.0\",\r\n		\"unit\": \"PCS\"\r\n	}\r\n	]\r\n}\r\n', '{\"Code\":400,\"Data\":null,\"Msg\":\"创建任务的时候出现异常值不能为 null。\\r\\n参数名: path\"}', 'Error', '', '', '2019-10-18 10:56:36', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('4', 'TaskAssign', '{\r\n	\"taskNo\": \"201910180001\",\r\n    \"taskType\": \"100\", \r\n    \"containerCode\": \"M00001\",\r\n	\"fromPort\": \"1001\",\r\n	\"toPort\": \"1001\",\r\n    \"fromLocationCode\": \"L010203\",\r\n    \"toLocationCode\": \"L010203\",\r\n	\"priority\": \"100\",\r\n	\"remark\": \"0\",\r\n	\"platform\": \"wms\",\r\n	\"taskDetails\": [{\r\n		\"referLineNo\": \"001\",\r\n        \"materialCode\": \"001201\",\r\n		\"materialName\": \"物料名称\",\r\n		\"qty\": \"0.0\",\r\n		\"unit\": \"PCS\"\r\n	}\r\n	]\r\n}\r\n', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:1001不在系统的管制范围内\"}', 'Error', '', '', '2019-10-18 11:07:13', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('5', 'TaskAssign', '{\r\n	\"taskNo\": \"201910180001\",\r\n    \"taskType\": \"100\", \r\n    \"containerCode\": \"M00001\",\r\n	\"fromPort\": \"1001\",\r\n	\"toPort\": \"1001\",\r\n    \"fromLocationCode\": \"L010203\",\r\n    \"toLocationCode\": \"L010203\",\r\n	\"priority\": \"100\",\r\n	\"remark\": \"0\",\r\n	\"platform\": \"wms\",\r\n	\"taskDetails\": [{\r\n		\"referLineNo\": \"001\",\r\n        \"materialCode\": \"001201\",\r\n		\"materialName\": \"物料名称\",\r\n		\"qty\": \"0.0\",\r\n		\"unit\": \"PCS\"\r\n	}\r\n	]\r\n}\r\n', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:1001不在系统的管制范围内\"}', 'Error', '', '', '2019-10-18 13:31:48', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('6', 'TaskAssign', '{\r\n	\"taskNo\": \"201910180001\",\r\n    \"taskType\": \"100\", \r\n    \"containerCode\": \"M00001\",\r\n	\"fromPort\": \"1001\",\r\n	\"toPort\": \"1001\",\r\n    \"fromLocationCode\": \"L010203\",\r\n    \"toLocationCode\": \"L010203\",\r\n	\"priority\": \"100\",\r\n	\"remark\": \"0\",\r\n	\"platform\": \"wms\",\r\n	\"taskDetails\": [{\r\n		\"referLineNo\": \"001\",\r\n        \"materialCode\": \"001201\",\r\n		\"materialName\": \"物料名称\",\r\n		\"qty\": \"0.0\",\r\n		\"unit\": \"PCS\"\r\n	}\r\n	]\r\n}\r\n', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:1001不在系统的管制范围内\"}', 'Error', '', '', '2019-10-18 14:14:29', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('7', 'TaskAssign', '{	\"taskNo\": \"201910180001\",\r\n    \"taskType\": \"100\", \r\n    \"containerCode\": \"M00001\",\r\n	\"fromPort\": \"1001\",\r\n	\"toPort\": \"1001\",\r\n    \"fromLocationCode\": \"L010203\",\r\n    \"toLocationCode\": \"L010203\",\r\n	\"priority\": \"100\",\r\n	\"remark\": \"0\",\r\n	\"platform\": \"wms\",\r\n	\"taskDetails\": [{\r\n		\"referLineNo\": \"001\",\r\n        \"materialCode\": \"001201\",\r\n		\"materialName\": \"物料名称\",\r\n		\"qty\": \"0.0\",\r\n		\"unit\": \"PCS\"\r\n	}\r\n	]}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:1001不在系统的管制范围内\"}', 'Error', '', '', '2019-10-18 14:16:19', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('8', 'GetAllUser', '', '{\"Code\":200,\"Data\":[\"admin\",\"user1\",\"user2\"],\"Msg\":\"\"}', 'Success', '', '', '2019-10-31 10:36:49', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('9', 'GetAllUser', '', '{\"Code\":200,\"Data\":[\"admin\",\"user1\",\"user2\"],\"Msg\":\"\"}', 'Success', '', '', '2019-10-31 10:37:41', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('10', 'GetAllUser', '', '{\"Code\":200,\"Data\":[\"admin\",\"user1\",\"user2\"],\"Msg\":\"\"}', 'Success', '', '', '2019-10-31 10:40:23', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('11', 'GetAllUser', '', '{\"Code\":200,\"Data\":[\"admin\",\"user1\",\"user2\"],\"Msg\":\"\"}', 'Success', '', '', '2019-10-31 10:41:29', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('12', 'GetAllUser', '', '{\"Code\":200,\"Data\":[\"admin\",\"user1\",\"user2\"],\"Msg\":\"\"}', 'Success', '', '', '2019-10-31 10:41:41', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('13', 'GetAllUser', '', '{\"Code\":200,\"Data\":[\"admin\",\"user1\",\"user2\"],\"Msg\":\"\"}', 'Success', '', '', '2019-10-31 10:42:06', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('14', 'TaskAssign', '{\n	\"TaskNo\":\"\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务号出现为空或null\"}', 'Error', '', '', '2019-10-31 11:05:08', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('15', 'TaskAssign', '{\n	\"TaskNo\":\"\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务号出现为空或null\"}', 'Error', '', '', '2019-10-31 11:15:49', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('16', 'TaskAssign', '{\n	\"TaskNo\":\"11\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,容器条码出现为空\"}', 'Error', '', '', '2019-10-31 11:16:05', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('17', 'Login', '{\n	\"TaskNo\":\"11\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"用户名或密码错误\"}', 'Error', '', '', '2019-10-31 11:23:11', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('18', 'Login', '{\"userCode\":\"admin\", \"password\":\"123456\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"用户名或密码错误\"}', 'Error', '', '', '2019-10-31 11:29:30', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('19', 'Login', '{\"userCode\":\"admin\", \"password\":\"123456\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"用户名或密码错误\"}', 'Error', '', '', '2019-10-31 11:29:43', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('20', 'Login', '{\"userCode\":\"admin\", \"password\":\"123456\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"用户名或密码错误\"}', 'Error', '', '', '2019-10-31 11:57:48', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('21', 'Login', '{\"userCode\":\"admin\", \"password\":\"123456\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"用户名或密码错误\"}', 'Error', '', '', '2019-10-31 11:57:54', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('22', 'Login', '{\"userCode\":\"admin\", \"password\":\"123456\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"用户名或密码错误\"}', 'Error', '', '', '2019-10-31 11:58:09', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('23', 'Login', '{\"userCode\":\"admin\", \"password\":\"123456\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"用户名或密码错误\"}', 'Error', '', '', '2019-10-31 12:06:53', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('24', 'TaskAssign', '{\r\n	\"taskNo\": \"201910180001\",\r\n    \"taskType\": \"100\", \r\n    \"containerCode\": \"M00001\",\r\n	\"fromPort\": \"1001\",\r\n	\"toPort\": \"1001\",\r\n    \"fromLocationCode\": \"L010203\",\r\n    \"toLocationCode\": \"L010203\",\r\n	\"priority\": \"100\",\r\n	\"remark\": \"0\",\r\n	\"platform\": \"wms\",\r\n	\"taskDetails\": [{\r\n		\"referLineNo\": \"001\",\r\n        \"materialCode\": \"001201\",\r\n		\"materialName\": \"物料名称\",\r\n		\"qty\": \"0.0\",\r\n		\"unit\": \"PCS\"\r\n	}\r\n	]\r\n}\r\n', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:1001不在系统的管制范围内\"}', 'Error', '', '', '2019-11-01 16:40:40', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('25', 'TaskAssign', '{\r\n	\"taskNo\": \"201910180001\",\r\n    \"taskType\": \"100\", \r\n    \"containerCode\": \"M00001\",\r\n	\"fromPort\": \"1001\",\r\n	\"toPort\": \"1001\",\r\n    \"fromLocationCode\": \"L010203\",\r\n    \"toLocationCode\": \"L010203\",\r\n	\"priority\": \"100\",\r\n	\"remark\": \"0\",\r\n	\"platform\": \"wms\",\r\n	\"taskDetails\": [{\r\n		\"referLineNo\": \"001\",\r\n        \"materialCode\": \"001201\",\r\n		\"materialName\": \"物料名称\",\r\n		\"qty\": \"0.0\",\r\n		\"unit\": \"PCS\"\r\n	}\r\n	]\r\n}\r\n', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:1001不在系统的管制范围内\"}', 'Error', '', '', '2019-11-01 16:43:27', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('26', 'TaskAssign', '{\n	\"taskNo\":\"12\",\n	\"taskType\":\"100\",\n	\"palletNo\":\"M00001\",\n	\"station\":\"GH01\",\n	\"fromLocation\":\"3010101\",\n	\"toLocation\":\"3010201\",\n	\"platform\":\"WMS\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,容器条码出现为空\"}', 'Error', '', '', '2019-11-01 16:57:45', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('27', 'TaskAssign', '{\r\n	\"taskNo\": \"201910180001\",\r\n    \"taskType\": \"100\", \r\n    \"containerCode\": \"M00001\",\r\n	\"fromPort\": \"1001\",\r\n	\"toPort\": \"1001\",\r\n    \"fromLocationCode\": \"L010203\",\r\n    \"toLocationCode\": \"L010203\",\r\n	\"priority\": \"100\",\r\n	\"remark\": \"0\",\r\n	\"platform\": \"wms\",\r\n	\"taskDetails\": [{\r\n		\"referLineNo\": \"001\",\r\n        \"materialCode\": \"001201\",\r\n		\"materialName\": \"物料名称\",\r\n		\"qty\": \"0.0\",\r\n		\"unit\": \"PCS\"\r\n	}\r\n	]\r\n}\r\n', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:1001不在系统的管制范围内\"}', 'Error', '', '', '2019-11-01 16:59:36', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('28', 'TaskAssign', '{\n	\"taskNo\":\"12\",\n	\"taskType\":\"100\",\n	\"palletNo\":\"M00001\",\n	\"station\":\"GH01\",\n	\"fromLocation\":\"3010101\",\n	\"toLocation\":\"3010201\",\n	\"platform\":\"WMS\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,容器条码出现为空\"}', 'Error', '', '', '2019-11-01 17:05:57', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('29', 'TaskAssign', '{\"containerCode\":\"LK00108\",\"fromLocationCode\":\"\",\"fromPort\":\"0\",\"platform\":\"wms\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"109\",\"unit\":\"卷\"}],\"taskNo\":\"94\",\"taskType\":\"100\",\"toLocationCode\":\"LK01-01-02-01\",\"toPort\":\"0\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,库位编码:from:,to:LK01-01-02-01不能为空\"}', 'Error', '', '', '2019-11-01 17:31:10', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('30', 'TaskAssign', '{\"containerCode\":\"LK00108\",\"fromLocationCode\":\"\",\"fromPort\":\"0\",\"platform\":\"wms\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"109\",\"unit\":\"卷\"}],\"taskNo\":\"94\",\"taskType\":\"100\",\"toLocationCode\":\"LK01-01-02-01\",\"toPort\":\"0\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,库位编码:from:,to:LK01-01-02-01不能为空\"}', 'Error', '', '', '2019-11-01 17:38:28', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('31', 'TaskAssign', '{\"containerCode\":\"M00008\",\"fromLocationCode\":\"0\",\"fromPort\":\"0\",\"platform\":\"wms\",\"preTaskNo\":\"0\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"95\",\"unit\":\"PCS\"}],\"taskNo\":\"80\",\"taskType\":\"100\",\"toLocationCode\":\"L05-05-01-01\",\"toPort\":\"0\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:0不在系统的管制范围内\"}', 'Error', '', '', '2019-11-04 11:04:34', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('32', 'TaskAssign', '{\"containerCode\":\"M00008\",\"fromLocationCode\":\"0\",\"fromPort\":\"1000\",\"platform\":\"wms\",\"preTaskNo\":\"0\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"95\",\"unit\":\"PCS\"}],\"taskNo\":\"80\",\"taskType\":\"100\",\"toLocationCode\":\"L05-05-01-01\",\"toPort\":\"0\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:0不在系统的管制范围内\"}', 'Error', '', '', '2019-11-04 14:50:47', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('33', 'TaskAssign', '{\"containerCode\":\"M00008\",\"fromLocationCode\":\"0\",\"fromPort\":\"1000\",\"platform\":\"wms\",\"preTaskNo\":\"0\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"95\",\"unit\":\"PCS\"}],\"taskNo\":\"80\",\"taskType\":\"100\",\"toLocationCode\":\"L05-05-01-01\",\"toPort\":\"0\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"出入的数据不对,任务类型:100和任务目的操作口:0不在系统的管制范围内\"}', 'Error', '', '', '2019-11-04 14:57:34', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('34', 'TaskAssign', '{\"containerCode\":\"M00008\",\"fromLocationCode\":\"0\",\"fromPort\":\"1000\",\"platform\":\"wms\",\"preTaskNo\":\"0\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"95\",\"unit\":\"PCS\"}],\"taskNo\":\"80\",\"taskType\":\"100\",\"toLocationCode\":\"L05-05-01-01\",\"toPort\":\"0\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"生成任务数据不对，任务号:80,错误原因:出现异常:MySql.Data.MySqlClient.MySqlException (0x80004005): Table \'huahengwcs2.task\' doesn\'t exist\\r\\n   在 MySql.Data.MySqlClient.MySqlStream.ReadPacket()\\r\\n   在 MySql.Data.MySqlClient.NativeDriver.GetResult(Int32& affectedRow, Int64& insertedId)\\r\\n   在 MySql.Data.MySqlClient.Driver.GetResult(Int32 statementId, Int32& affectedRows, Int64& insertedId)\\r\\n   在 MySql.Data.MySqlClient.Driver.NextResult(Int32 statementId, Boolean force)\\r\\n   在 MySql.Data.MySqlClient.MySqlDataReader.NextResult()\\r\\n   在 MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior)\\r\\n   在 MySql.Data.MySqlClient.MySqlCommand.ExecuteDbDataReader(CommandBehavior behavior)\\r\\n   在 System.Data.Common.DbCommand.System.Data.IDbCommand.ExecuteReader(CommandBehavior behavior)\\r\\n   在 Dapper.SqlMapper.ExecuteReaderWithFlagsFallback(IDbCommand cmd, Boolean wasClosed, CommandBehavior behavior)\\r\\n   在 Dapper.SqlMapper.<QueryImpl>d__138`1.MoveNext()\\r\\n   在 System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)\\r\\n   在 System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)\\r\\n   在 Dapper.SqlMapper.Query[T](IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Boolean buffered, Nullable`1 commandTimeout, Nullable`1 commandType)\\r\\n   在 HHECS.Bll.TaskService.CreateTask(IDbConnection connection, IDbTransaction trans, String remoteTaskNo, String palletCode, String port, String gataway, String fromLocation, String toLocation, Int32 status, Int32 type, Int32 priority, Int32 preTaskId, String userCode, String warehouseCode, String platform)\"}', 'Error', '', '', '2019-11-04 15:01:56', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('35', 'TaskAssign', '{\"containerCode\":\"M00008\",\"fromLocationCode\":\"0\",\"fromPort\":\"1000\",\"platform\":\"wms\",\"preTaskNo\":\"0\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"95\",\"unit\":\"PCS\"}],\"taskNo\":\"80\",\"taskType\":\"100\",\"toLocationCode\":\"L05-05-01-01\",\"toPort\":\"0\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"生成任务数据不对，任务号:80,错误原因:没有找到库位\"}', 'Error', '', '', '2019-11-04 15:32:33', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('36', 'TaskAssign', '{\"containerCode\":\"M00008\",\"fromLocationCode\":\"0\",\"fromPort\":\"1000\",\"platform\":\"wms\",\"preTaskNo\":\"0\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"95\",\"unit\":\"PCS\"}],\"taskNo\":\"80\",\"taskType\":\"100\",\"toLocationCode\":\"L01-01-01-01\",\"toPort\":\"0\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"生成任务数据不对，任务号:80,错误原因:没有找到库位\"}', 'Error', '', '', '2019-11-04 15:34:25', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('37', 'TaskAssign', '{\"containerCode\":\"M00008\",\"fromLocationCode\":\"0\",\"fromPort\":\"1000\",\"platform\":\"wms\",\"preTaskNo\":\"0\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"95\",\"unit\":\"PCS\"}],\"taskNo\":\"80\",\"taskType\":\"100\",\"toLocationCode\":\"L01-01-01-01\",\"toPort\":\"0\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"生成任务数据不对，任务号:80,错误原因:没有找到库位:L01-01-01-01\"}', 'Error', '', '', '2019-11-04 15:38:55', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('38', 'TaskAssign', '{\"containerCode\":\"M00008\",\"fromLocationCode\":\"0\",\"fromPort\":\"1000\",\"platform\":\"wms\",\"preTaskNo\":\"0\",\"priority\":100,\"remark\":\"0\",\"taskDetails\":[{\"materialCode\":\"13904000103\",\"materialName\":\"黑色扁平电缆（2.5mm）\",\"qty\":10,\"referLineNo\":\"95\",\"unit\":\"PCS\"}],\"taskNo\":\"80\",\"taskType\":\"100\",\"toLocationCode\":\"L01-01-01-01\",\"toPort\":\"0\"}', '{\"Code\":200,\"Data\":null,\"Msg\":\"\"}', 'Success', '', '', '2019-11-04 15:53:35', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('39', 'TaskInfo', '{\n	\n	\"taskNo\":\"80\"\n	\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 15:56:19', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('40', 'TaskInfo', '{\n	\n	\"taskNo\":\"80\"\n	\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 15:57:10', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('41', 'TaskInfo', '{\n	\"taskNo\":\"80\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 16:05:09', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('42', 'TaskInfo', '{\n	\"taskNo\":\"80\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 16:07:10', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('43', 'TaskInfo', '{\n	\"taskNo\":\"80\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 16:12:13', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('44', 'TaskInfo', '{\n	\"taskNo\":\"80\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 16:12:13', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('45', 'TaskInfo', '{\n	\"taskNo\":\"80\"\n}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 16:12:23', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('46', 'TaskInfo', '{\"priority\":0,\"taskNo\":\"80\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 16:19:15', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('47', 'TaskInfo', '{\"priority\":0,\"taskNo\":\"80\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 16:22:30', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('48', 'TaskInfo', '{\"taskNo\":\"80\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 16:22:30', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('49', 'TaskInfo', '\"80\"', '{\"Code\":200,\"Data\":{\"taskNo\":\"80\",\"taskStatus\":1,\"taskStatusDesc\":\"生成任务\",\"currentEquipmentName\":null},\"Msg\":\"\"}', 'Success', '', '', '2019-11-04 16:23:57', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('50', 'TaskInfo', '{\r\n	\"taskNo\": \"80\"\r\n   \r\n}\r\n', '{\"Code\":400,\"Data\":null,\"Msg\":\"\"}', 'Error', '', '', '2019-11-04 16:37:14', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('51', 'TaskInfo', '{\"taskNo\":\"80\"}', '{\"Code\":200,\"Data\":{\"taskNo\":\"80\",\"taskStatus\":1,\"taskStatusDesc\":\"生成任务\",\"currentEquipmentName\":null},\"Msg\":\"\"}', 'Success', '', '', '2019-11-04 16:47:41', 'WCSInterface', null, null);
INSERT INTO `wcsinterfacelog` VALUES ('52', 'TaskCancel', '{\"taskNo\":\"80\"}', '{\"Code\":400,\"Data\":null,\"Msg\":\"取消任务出现异常：The connection is not open.\"}', 'Error', '', '', '2019-11-04 17:06:38', 'WCSInterface', null, null);

-- ----------------------------
-- Table structure for wcslocation
-- ----------------------------
DROP TABLE IF EXISTS `wcslocation`;
CREATE TABLE `wcslocation` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `code` varchar(50) NOT NULL DEFAULT '' COMMENT '库位',
  `linkCode` varchar(50) DEFAULT NULL COMMENT '关联库位编码',
  `row` smallint(6) NOT NULL COMMENT '行',
  `column` smallint(6) NOT NULL COMMENT '列',
  `layer` smallint(6) NOT NULL COMMENT '层',
  `grid` smallint(6) NOT NULL DEFAULT '0' COMMENT '格',
  `rowIndex` smallint(6) NOT NULL DEFAULT '0' COMMENT '双伸位索引',
  `roadway` smallint(11) NOT NULL DEFAULT '1' COMMENT '巷道',
  `type` varchar(50) DEFAULT NULL COMMENT '库位类型',
  `containerCode` varchar(50) NOT NULL DEFAULT '' COMMENT '容器编码',
  `status` smallint(6) NOT NULL DEFAULT '0',
  `zoneCode` varchar(50) DEFAULT '' COMMENT '区域编码',
  `warehouseCode` varchar(50) NOT NULL DEFAULT '' COMMENT '仓库编码',
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) DEFAULT '' COMMENT '创建用户',
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) DEFAULT '' COMMENT '更新用户',
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`,`warehouseCode`) USING BTREE,
  UNIQUE KEY `id` (`id`) USING BTREE,
  KEY `linkCode` (`linkCode`) USING HASH
) ENGINE=InnoDB AUTO_INCREMENT=712 DEFAULT CHARSET=utf8mb4 COMMENT='库位表';

-- ----------------------------
-- Records of wcslocation
-- ----------------------------
INSERT INTO `wcslocation` VALUES ('74', 'L01-01-01', null, '1', '1', '1', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('75', 'L01-01-02', null, '1', '1', '2', '0', '2', '1', 'L', 'M00002', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('76', 'L01-01-03', null, '1', '1', '3', '0', '2', '1', 'L', 'M00336', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('77', 'L01-01-04', null, '1', '1', '4', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('78', 'L01-01-05', null, '1', '1', '5', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('79', 'L01-02-01', null, '1', '2', '1', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('80', 'L01-02-02', null, '1', '2', '2', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('81', 'L01-02-03', null, '1', '2', '3', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('82', 'L01-02-04', null, '1', '2', '4', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('83', 'L01-02-05', null, '1', '2', '5', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('84', 'L01-03-01', null, '1', '3', '1', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('85', 'L01-03-02', null, '1', '3', '2', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('86', 'L01-03-03', null, '1', '3', '3', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('87', 'L01-03-04', null, '1', '3', '4', '0', '2', '1', 'L', 'M00335', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('88', 'L01-03-05', null, '1', '3', '5', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('89', 'L01-04-01', null, '1', '4', '1', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('90', 'L01-04-02', null, '1', '4', '2', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('91', 'L01-04-03', null, '1', '4', '3', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('92', 'L01-04-04', null, '1', '4', '4', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('93', 'L01-04-05', null, '1', '4', '5', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('94', 'L01-05-01', null, '1', '5', '1', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('95', 'L01-05-02', null, '1', '5', '2', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('96', 'L01-05-03', null, '1', '5', '3', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('97', 'L01-05-04', null, '1', '5', '4', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('98', 'L01-05-05', null, '1', '5', '5', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('99', 'L01-06-01', null, '1', '6', '1', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('100', 'L01-06-02', null, '1', '6', '2', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('101', 'L01-06-03', null, '1', '6', '3', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('102', 'L01-06-04', null, '1', '6', '4', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('103', 'L01-06-05', null, '1', '6', '5', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('104', 'L01-07-01', null, '1', '7', '1', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('105', 'L01-07-02', null, '1', '7', '2', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('106', 'L01-07-03', null, '1', '7', '3', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('107', 'L01-07-04', null, '1', '7', '4', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('108', 'L01-07-05', null, '1', '7', '5', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('109', 'L01-08-01', null, '1', '8', '1', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('110', 'L01-08-02', null, '1', '8', '2', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('111', 'L01-08-03', null, '1', '8', '3', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('112', 'L01-08-04', null, '1', '8', '4', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('113', 'L01-08-05', null, '1', '8', '5', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('114', 'L01-09-01', null, '1', '9', '1', '0', '2', '1', 'L', 'M00044', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('115', 'L01-09-02', null, '1', '9', '2', '0', '2', '1', 'L', 'M00045', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('116', 'L01-09-03', null, '1', '9', '3', '0', '2', '1', 'L', 'M00047', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('117', 'L01-09-04', null, '1', '9', '4', '0', '2', '1', 'L', 'M00048', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('118', 'L01-09-05', null, '1', '9', '5', '0', '2', '1', 'L', 'M00049', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('119', 'L01-10-01', null, '1', '10', '1', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('120', 'L01-10-02', null, '1', '10', '2', '0', '2', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('121', 'L01-10-03', null, '1', '10', '3', '0', '2', '1', 'L', 'M00039', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('122', 'L01-10-04', null, '1', '10', '4', '0', '2', '1', 'L', 'M00040', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('123', 'L01-10-05', null, '1', '10', '5', '0', '2', '1', 'L', 'M00041', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('124', 'L01-11-01', null, '1', '11', '1', '0', '2', '1', 'L', 'M00073', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('125', 'L01-11-02', null, '1', '11', '2', '0', '2', '1', 'L', 'M00074', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('126', 'L01-11-03', null, '1', '11', '3', '0', '2', '1', 'L', 'M00075', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('127', 'L01-11-04', null, '1', '11', '4', '0', '2', '1', 'L', 'M00081', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('128', 'L01-11-05', null, '1', '11', '5', '0', '2', '1', 'L', 'M00082', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('129', 'L01-12-01', null, '1', '12', '1', '0', '2', '1', 'L', 'M00021', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('130', 'L01-12-02', null, '1', '12', '2', '0', '2', '1', 'L', 'M00023', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('131', 'L01-12-03', null, '1', '12', '3', '0', '2', '1', 'L', 'M00061', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('132', 'L01-12-04', null, '1', '12', '4', '0', '2', '1', 'L', 'M00111', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('133', 'L01-12-05', null, '1', '12', '5', '0', '2', '1', 'L', 'M00062', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('134', 'L01-13-01', null, '1', '13', '1', '0', '2', '1', 'L', 'S00020', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('135', 'L01-13-02', null, '1', '13', '2', '0', '2', '1', 'L', 'M00356', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('136', 'L01-13-03', null, '1', '13', '3', '0', '2', '1', 'L', 'M00355', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('137', 'L01-13-04', null, '1', '13', '4', '0', '2', '1', 'L', 'M00354', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('138', 'L01-13-05', null, '1', '13', '5', '0', '2', '1', 'L', 'M00353', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('139', 'L01-14-01', null, '1', '14', '1', '0', '2', '1', 'L', 'M00359', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('140', 'L01-14-02', null, '1', '14', '2', '0', '2', '1', 'L', 'M00015', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('141', 'L01-14-03', null, '1', '14', '3', '0', '2', '1', 'L', 'M00374', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('142', 'L01-14-04', null, '1', '14', '4', '0', '2', '1', 'L', 'M00060', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('143', 'L01-14-05', null, '1', '14', '5', '0', '2', '1', 'L', 'M00072', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('234', 'L02-01-01', null, '2', '1', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('235', 'L02-01-02', null, '2', '1', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('236', 'L02-01-03', null, '2', '1', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('237', 'L02-01-04', null, '2', '1', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('238', 'L02-01-05', null, '2', '1', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('239', 'L02-02-01', null, '2', '2', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('240', 'L02-02-02', null, '2', '2', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('241', 'L02-02-03', null, '2', '2', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('242', 'L02-02-04', null, '2', '2', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('243', 'L02-02-05', null, '2', '2', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('244', 'L02-03-01', null, '2', '3', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('245', 'L02-03-02', null, '2', '3', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('246', 'L02-03-03', null, '2', '3', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('247', 'L02-03-04', null, '2', '3', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('248', 'L02-03-05', null, '2', '3', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('249', 'L02-04-01', null, '2', '4', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('250', 'L02-04-02', null, '2', '4', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('251', 'L02-04-03', null, '2', '4', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('252', 'L02-04-04', null, '2', '4', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('253', 'L02-04-05', null, '2', '4', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('254', 'L02-05-01', null, '2', '5', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('255', 'L02-05-02', null, '2', '5', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('256', 'L02-05-03', null, '2', '5', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('257', 'L02-05-04', null, '2', '5', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('258', 'L02-05-05', null, '2', '5', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('259', 'L02-06-01', null, '2', '6', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('260', 'L02-06-02', null, '2', '6', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('261', 'L02-06-03', null, '2', '6', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('262', 'L02-06-04', null, '2', '6', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('263', 'L02-06-05', null, '2', '6', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('264', 'L02-07-01', null, '2', '7', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('265', 'L02-07-02', null, '2', '7', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('266', 'L02-07-03', null, '2', '7', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('267', 'L02-07-04', null, '2', '7', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('268', 'L02-07-05', null, '2', '7', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('269', 'L02-08-01', null, '2', '8', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('270', 'L02-08-02', null, '2', '8', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('271', 'L02-08-03', null, '2', '8', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('272', 'L02-08-04', null, '2', '8', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('273', 'L02-08-05', null, '2', '8', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('274', 'L02-09-01', null, '2', '9', '1', '0', '1', '1', 'L', 'M00002', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('275', 'L02-09-02', null, '2', '9', '2', '0', '1', '1', 'L', 'M00051', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('276', 'L02-09-03', null, '2', '9', '3', '0', '1', '1', 'L', 'M00052', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('277', 'L02-09-04', null, '2', '9', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('278', 'L02-09-05', null, '2', '9', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('279', 'L02-10-01', null, '2', '10', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('280', 'L02-10-02', null, '2', '10', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('281', 'L02-10-03', null, '2', '10', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('282', 'L02-10-04', null, '2', '10', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('283', 'L02-10-05', null, '2', '10', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('284', 'L02-11-01', null, '2', '11', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('285', 'L02-11-02', null, '2', '11', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('286', 'L02-11-03', null, '2', '11', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('287', 'L02-11-04', null, '2', '11', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('288', 'L02-11-05', null, '2', '11', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('289', 'L02-12-01', null, '2', '12', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('290', 'L02-12-02', null, '2', '12', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('291', 'L02-12-03', null, '2', '12', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('292', 'L02-12-04', null, '2', '12', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('293', 'L02-12-05', null, '2', '12', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('294', 'L02-13-01', null, '2', '13', '1', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('295', 'L02-13-02', null, '2', '13', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('296', 'L02-13-03', null, '2', '13', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('297', 'L02-13-04', null, '2', '13', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('298', 'L02-13-05', null, '2', '13', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('299', 'L02-14-01', null, '2', '14', '1', '0', '1', '1', 'L', 'M00050', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('300', 'L02-14-02', null, '2', '14', '2', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('301', 'L02-14-03', null, '2', '14', '3', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('302', 'L02-14-04', null, '2', '14', '4', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('303', 'L02-14-05', null, '2', '14', '5', '0', '1', '1', 'L', '', '0', 'LK', 'A0001', null, null, null, null);
INSERT INTO `wcslocation` VALUES ('416', 'L03-01-01', null, '3', '1', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('417', 'L03-01-02', null, '3', '1', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('418', 'L03-01-03', null, '3', '1', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('419', 'L03-01-04', null, '3', '1', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('420', 'L03-01-05', null, '3', '1', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('421', 'L03-02-01', null, '3', '2', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('422', 'L03-02-02', null, '3', '2', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('423', 'L03-02-03', null, '3', '2', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('424', 'L03-02-04', null, '3', '2', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('425', 'L03-02-05', null, '3', '2', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('426', 'L03-03-01', null, '3', '3', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('427', 'L03-03-02', null, '3', '3', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('428', 'L03-03-03', null, '3', '3', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('429', 'L03-03-04', null, '3', '3', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('430', 'L03-03-05', null, '3', '3', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('431', 'L03-04-01', null, '3', '4', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('432', 'L03-04-02', null, '3', '4', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('433', 'L03-04-03', null, '3', '4', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('434', 'L03-04-04', null, '3', '4', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('435', 'L03-04-05', null, '3', '4', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('436', 'L03-05-01', null, '3', '5', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('437', 'L03-05-02', null, '3', '5', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('438', 'L03-05-03', null, '3', '5', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('439', 'L03-05-04', null, '3', '5', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('440', 'L03-05-05', null, '3', '5', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('441', 'L03-06-01', null, '3', '6', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('442', 'L03-06-02', null, '3', '6', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('443', 'L03-06-03', null, '3', '6', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('444', 'L03-06-04', null, '3', '6', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('445', 'L03-06-05', null, '3', '6', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('446', 'L03-07-01', null, '3', '7', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('447', 'L03-07-02', null, '3', '7', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('448', 'L03-07-03', null, '3', '7', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('449', 'L03-07-04', null, '3', '7', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('450', 'L03-07-05', null, '3', '7', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('451', 'L03-08-01', null, '3', '8', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('452', 'L03-08-02', null, '3', '8', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('453', 'L03-08-03', null, '3', '8', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('454', 'L03-08-04', null, '3', '8', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('455', 'L03-08-05', null, '3', '8', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('456', 'L03-09-01', null, '3', '9', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('457', 'L03-09-02', null, '3', '9', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('458', 'L03-09-03', null, '3', '9', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('459', 'L03-09-04', null, '3', '9', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('460', 'L03-09-05', null, '3', '9', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('461', 'L03-10-01', null, '3', '10', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('462', 'L03-10-02', null, '3', '10', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('463', 'L03-10-03', null, '3', '10', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('464', 'L03-10-04', null, '3', '10', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('465', 'L03-10-05', null, '3', '10', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('466', 'L03-11-01', null, '3', '11', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('467', 'L03-11-02', null, '3', '11', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('468', 'L03-11-03', null, '3', '11', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('469', 'L03-11-04', null, '3', '11', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('470', 'L03-11-05', null, '3', '11', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('471', 'L03-12-01', null, '3', '12', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('472', 'L03-12-02', null, '3', '12', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('473', 'L03-12-03', null, '3', '12', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('474', 'L03-12-04', null, '3', '12', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('475', 'L03-12-05', null, '3', '12', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('476', 'L03-13-01', null, '3', '13', '1', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('477', 'L03-13-02', null, '3', '13', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('478', 'L03-13-03', null, '3', '13', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('479', 'L03-13-04', null, '3', '13', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('480', 'L03-13-05', null, '3', '13', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('481', 'L03-14-01', null, '3', '14', '1', '0', '3', '1', 'L', 'M00200', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('482', 'L03-14-02', null, '3', '14', '2', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('483', 'L03-14-03', null, '3', '14', '3', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('484', 'L03-14-04', null, '3', '14', '4', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('485', 'L03-14-05', null, '3', '14', '5', '0', '3', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('543', 'L04-01-01', null, '4', '1', '1', '0', '4', '1', 'L', 'M00002', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('544', 'L04-01-02', null, '4', '1', '2', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('545', 'L04-01-03', null, '4', '1', '3', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('546', 'L04-01-04', null, '4', '1', '4', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('547', 'L04-01-05', null, '4', '1', '5', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('548', 'L04-02-01', null, '4', '2', '1', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('549', 'L04-02-02', null, '4', '2', '2', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('550', 'L04-02-03', null, '4', '2', '3', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('551', 'L04-02-04', null, '4', '2', '4', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('552', 'L04-02-05', null, '4', '2', '5', '0', '4', '1', 'L', 'M00395', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('553', 'L04-03-01', null, '4', '3', '1', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('554', 'L04-03-02', null, '4', '3', '2', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('555', 'L04-03-03', null, '4', '3', '3', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('556', 'L04-03-04', null, '4', '3', '4', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('557', 'L04-03-05', null, '4', '3', '5', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('558', 'L04-04-01', null, '4', '4', '1', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('559', 'L04-04-02', null, '4', '4', '2', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('560', 'L04-04-03', null, '4', '4', '3', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('561', 'L04-04-04', null, '4', '4', '4', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('562', 'L04-04-05', null, '4', '4', '5', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('563', 'L04-05-01', null, '4', '5', '1', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('564', 'L04-05-02', null, '4', '5', '2', '0', '4', '1', 'L', 'M00396', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('565', 'L04-05-03', null, '4', '5', '3', '0', '4', '1', 'L', 'M00400', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('566', 'L04-05-04', null, '4', '5', '4', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('567', 'L04-05-05', null, '4', '5', '5', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('568', 'L04-06-01', null, '4', '6', '1', '0', '4', '1', 'L', 'M00071', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('569', 'L04-06-02', null, '4', '6', '2', '0', '4', '1', 'L', 'M00398', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('570', 'L04-06-03', null, '4', '6', '3', '0', '4', '1', 'L', 'M00399', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('571', 'L04-06-04', null, '4', '6', '4', '0', '4', '1', 'L', 'M00387', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('572', 'L04-06-05', null, '4', '6', '5', '0', '4', '1', 'L', 'M00385', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('573', 'L04-07-01', null, '4', '7', '1', '0', '4', '1', 'L', 'M00393', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('574', 'L04-07-02', null, '4', '7', '2', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('575', 'L04-07-03', null, '4', '7', '3', '0', '4', '1', 'L', 'M00382', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('576', 'L04-07-04', null, '4', '7', '4', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('577', 'L04-07-05', null, '4', '7', '5', '0', '4', '1', 'L', 'L00320', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('578', 'L04-08-01', null, '4', '8', '1', '0', '4', '1', 'L', 'M00076', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('579', 'L04-08-02', null, '4', '8', '2', '0', '4', '1', 'L', 'S00018', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('580', 'L04-08-03', null, '4', '8', '3', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('581', 'L04-08-04', null, '4', '8', '4', '0', '4', '1', 'L', 'M00386', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('582', 'L04-08-05', null, '4', '8', '5', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('583', 'L04-09-01', null, '4', '9', '1', '0', '4', '1', 'L', 'M00042', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('584', 'L04-09-02', null, '4', '9', '2', '0', '4', '1', 'L', 'M00379', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('585', 'L04-09-03', null, '4', '9', '3', '0', '4', '1', 'L', 'M00380', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('586', 'L04-09-04', null, '4', '9', '4', '0', '4', '1', 'L', 'S00015', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('587', 'L04-09-05', null, '4', '9', '5', '0', '4', '1', 'L', 'M00043', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('588', 'L04-10-01', null, '4', '10', '1', '0', '4', '1', 'L', 'M00402', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('589', 'L04-10-02', null, '4', '10', '2', '0', '4', '1', 'L', 'M00391', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('590', 'L04-10-03', null, '4', '10', '3', '0', '4', '1', 'L', 'M00392', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('591', 'L04-10-04', null, '4', '10', '4', '0', '4', '1', 'L', 'M00083', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('592', 'L04-10-05', null, '4', '10', '5', '0', '4', '1', 'L', 'M00394', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('593', 'L04-11-01', null, '4', '11', '1', '0', '4', '1', 'L', 'M00063', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('594', 'L04-11-02', null, '4', '11', '2', '0', '4', '1', 'L', 'M00030', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('595', 'L04-11-03', null, '4', '11', '3', '0', '4', '1', 'L', 'M00018', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('596', 'L04-11-04', null, '4', '11', '4', '0', '4', '1', 'L', 'S00022', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('597', 'L04-11-05', null, '4', '11', '5', '0', '4', '1', 'L', 'M00367', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('598', 'L04-12-01', null, '4', '12', '1', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('599', 'L04-12-02', null, '4', '12', '2', '0', '4', '1', 'L', 'M00339', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('600', 'L04-12-03', null, '4', '12', '3', '0', '4', '1', 'L', 'M00352', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('601', 'L04-12-04', null, '4', '12', '4', '0', '4', '1', 'L', 'M00351', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('602', 'L04-12-05', null, '4', '12', '5', '0', '4', '1', 'L', 'M00384', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('603', 'L04-13-01', null, '4', '13', '1', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('604', 'L04-13-02', null, '4', '13', '2', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('605', 'L04-13-03', null, '4', '13', '3', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('606', 'L04-13-04', null, '4', '13', '4', '0', '4', '1', 'L', 'M00369', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('607', 'L04-13-05', null, '4', '13', '5', '0', '4', '1', 'L', 'S00021', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('608', 'L04-14-01', null, '4', '14', '1', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('609', 'L04-14-02', null, '4', '14', '2', '0', '4', '1', 'L', 'M00405', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('610', 'L04-14-03', null, '4', '14', '3', '0', '4', '1', 'L', 'M00404', '0', 'LK', 'A0001', null, 'ricard', null, 'ricard');
INSERT INTO `wcslocation` VALUES ('611', 'L04-14-04', null, '4', '14', '4', '0', '4', '1', 'L', 'M00403', '0', 'LK', 'A0001', null, 'ricard', null, 'my test');
INSERT INTO `wcslocation` VALUES ('612', 'L04-14-05', null, '4', '14', '5', '0', '4', '1', 'L', '', '0', 'LK', 'A0001', null, 'ricard', null, 'my test');
INSERT INTO `wcslocation` VALUES ('613', 'L01-01-01-01', null, '1', '1', '1', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('614', 'L01-01-02-02', null, '1', '1', '2', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('615', 'L01-01-03-03', null, '1', '1', '3', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('616', 'L01-01-04-04', null, '1', '1', '4', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('617', 'L01-01-05-05', null, '1', '1', '5', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('618', 'L01-01-06-06', null, '1', '1', '6', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('619', 'L01-01-07-07', null, '1', '1', '7', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('620', 'L01-01-08-08', null, '1', '1', '8', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('621', 'L01-01-09-09', null, '1', '1', '9', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('622', 'L01-01-10-10', null, '1', '1', '10', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('623', 'L01-01-11-11', null, '1', '1', '11', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('624', 'L01-01-12-12', null, '1', '1', '12', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('625', 'L01-01-13-13', null, '1', '1', '13', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('626', 'L01-01-14-14', null, '1', '1', '14', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('627', 'L01-01-15-15', null, '1', '1', '15', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('628', 'L01-01-16-16', null, '1', '1', '16', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('629', 'L01-01-17-17', null, '1', '1', '17', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('630', 'L01-01-18-18', null, '1', '1', '18', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('631', 'L01-01-19-19', null, '1', '1', '19', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('632', 'L01-01-20-20', null, '1', '1', '20', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('633', 'L01-01-21-21', null, '1', '1', '21', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('634', 'L01-01-22-22', null, '1', '1', '22', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('635', 'L01-01-23-23', null, '1', '1', '23', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('636', 'L01-01-24-24', null, '1', '1', '24', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('637', 'L01-01-25-25', null, '1', '1', '25', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('638', 'L01-01-26-26', null, '1', '1', '26', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('639', 'L01-01-27-27', null, '1', '1', '27', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('640', 'L01-01-28-28', null, '1', '1', '28', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('641', 'L01-01-29-29', null, '1', '1', '29', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('642', 'L01-01-30-30', null, '1', '1', '30', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('643', 'L01-01-31-31', null, '1', '1', '31', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('644', 'L01-01-32-32', null, '1', '1', '32', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('645', 'L01-01-33-33', null, '1', '1', '33', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('646', 'L01-01-34-34', null, '1', '1', '34', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('647', 'L01-01-35-35', null, '1', '1', '35', '1', '0', '1', 'L', 'M00401', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('648', 'L01-01-36-36', null, '1', '1', '36', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('649', 'L01-01-37-37', null, '1', '1', '37', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('650', 'L01-01-38-38', null, '1', '1', '38', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('651', 'L01-01-39-39', null, '1', '1', '39', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('652', 'L01-01-40-40', null, '1', '1', '40', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('653', 'L01-01-41-41', null, '1', '1', '41', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('654', 'L01-01-42-42', null, '1', '1', '42', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('655', 'L01-01-43-43', null, '1', '1', '43', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('656', 'L01-01-44-44', null, '1', '1', '44', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('657', 'L01-01-45-45', null, '1', '1', '45', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('658', 'L01-01-46-46', null, '1', '1', '46', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('659', 'L01-01-47-47', null, '1', '1', '47', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('660', 'L01-01-48-48', null, '1', '1', '48', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('661', 'L01-01-49-49', null, '1', '1', '49', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('662', 'L01-01-50-50', null, '1', '1', '50', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('663', 'L01-01-51-51', null, '1', '1', '51', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('664', 'L01-01-52-52', null, '1', '1', '52', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('665', 'L01-01-53-53', null, '1', '1', '53', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('666', 'L01-01-54-54', null, '1', '1', '54', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('667', 'L01-01-55-55', null, '1', '1', '55', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('668', 'L01-01-56-56', null, '1', '1', '56', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('669', 'L01-01-57-57', null, '1', '1', '57', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('670', 'L01-01-58-58', null, '1', '1', '58', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('671', 'L01-01-59-59', null, '1', '1', '59', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('672', 'L01-01-60-60', null, '1', '1', '60', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('673', 'L01-01-61-61', null, '1', '1', '61', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('674', 'L01-01-62-62', null, '1', '1', '62', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('675', 'L01-01-63-63', null, '1', '1', '63', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('676', 'L01-01-64-64', null, '1', '1', '64', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('677', 'L01-01-65-65', null, '1', '1', '65', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('678', 'L01-01-66-66', null, '1', '1', '66', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('679', 'L01-01-67-67', null, '1', '1', '67', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('680', 'L01-01-68-68', null, '1', '1', '68', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('681', 'L01-01-69-69', null, '1', '1', '69', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('682', 'L01-01-70-70', null, '1', '1', '70', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('683', 'L01-01-71-71', null, '1', '1', '71', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('684', 'L01-01-72-72', null, '1', '1', '72', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('685', 'L01-01-73-73', null, '1', '1', '73', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('686', 'L01-01-74-74', null, '1', '1', '74', '1', '0', '1', 'L', 'S00019', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('687', 'L01-01-75-75', null, '1', '1', '75', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('688', 'L01-01-76-76', null, '1', '1', '76', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('689', 'L01-01-77-77', null, '1', '1', '77', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('690', 'L01-01-78-78', null, '1', '1', '78', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('691', 'L01-01-79-79', null, '1', '1', '79', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('692', 'L01-01-80-80', null, '1', '1', '80', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('693', 'L01-01-81-81', null, '1', '1', '81', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('694', 'L01-01-82-82', null, '1', '1', '82', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');
INSERT INTO `wcslocation` VALUES ('695', 'L01-01-83-83', null, '1', '1', '83', '1', '0', '1', 'L', '', '0', 'LK', 'A0001', null, 'tony', null, 'tony');

-- ----------------------------
-- Table structure for wcsmenuoperation
-- ----------------------------
DROP TABLE IF EXISTS `wcsmenuoperation`;
CREATE TABLE `wcsmenuoperation` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `menuName` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `parentId` int(11) DEFAULT NULL,
  `orderNum` int(11) DEFAULT NULL,
  `url` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `menuType` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `icon` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `perms` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `remark` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `visible` tinyint(4) DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` date DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=79 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsmenuoperation
-- ----------------------------
INSERT INTO `wcsmenuoperation` VALUES ('1', '系统', null, '0', null, 'catalog', '/Content/Icon/目录.png', '123', null, '0', null, null, '2018-11-01', '管理员');
INSERT INTO `wcsmenuoperation` VALUES ('3', '权限管理', '1', null, 'HHECS.View.UserOperation.WinPermission', 'menu', '/Content/Icon/菜单.png', 'menu:permission', 'ddddd', '0', '2018-10-24 14:38:44', null, null, '');
INSERT INTO `wcsmenuoperation` VALUES ('4', '任务', null, '40', '', 'catalog', '/Content/Icon/目录.png', '', '', '0', '2018-10-24 14:49:49', null, '2018-11-13', '管理员');
INSERT INTO `wcsmenuoperation` VALUES ('5', '容器', null, '20', '', 'catalog', '/Content/Icon/目录.png', '', '', '0', '2018-10-24 14:50:18', null, '2018-11-13', '管理员');
INSERT INTO `wcsmenuoperation` VALUES ('6', '任务管理', '4', null, 'HHECS.View.TaskInfo.WinTaskInfo', 'menu', '/Content/Icon/菜单.png', 'menu:task', '', '0', '2018-10-24 14:50:56', null, '2018-11-13', '管理员');
INSERT INTO `wcsmenuoperation` VALUES ('8', '系统参数', '1', null, 'HHECS.View.SystemInfo.WinConfig', 'menu', '/Content/Icon/菜单.png', '', '', '0', '2018-10-24 15:43:00', null, '2018-11-01', '管理员');
INSERT INTO `wcsmenuoperation` VALUES ('9', '数据字典', '1', null, 'HHECS.View.SystemInfo.WinDict', 'menu', '/Content/Icon/菜单.png', '', '', '0', '2018-10-24 15:44:02', null, '2018-11-01', '管理员');
INSERT INTO `wcsmenuoperation` VALUES ('10', '用户', null, '10', '', 'catalog', '/Content/Icon/目录.png', '', '', '0', '2018-10-24 15:44:58', null, '2018-11-13', '管理员');
INSERT INTO `wcsmenuoperation` VALUES ('11', '保存', '3', null, '#', 'button', '/Content/Icon/按钮.png', 'permission:save', '', '0', '2018-10-25 19:11:37', '管理员', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('12', '用户管理', '10', null, 'HHECS.View.UserOperation.WinUser', 'menu', '/Content/Icon/菜单.png', '', '', '0', '2018-10-25 19:55:58', '管理员', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('13', '查询', '12', null, '', 'button', '/Content/Icon/按钮.png', 'user:query', '', '0', '2018-10-26 15:22:28', '管理员', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('14', '新增', '12', null, '', 'button', '/Content/Icon/按钮.png', 'user:add', '', '0', '2018-10-26 15:22:48', '管理员', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('15', '角色管理', '10', null, 'HHECS.View.UserOperation.WinRole', 'menu', '/Content/Icon/菜单.png', '', '', '0', '2018-10-26 15:26:20', '管理员', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('16', '设备管理', null, '60', '', 'catalog', '/Content/Icon/目录.png', '', '', '0', '2018-10-29 14:21:23', '管理员', '2018-11-13', '管理员');
INSERT INTO `wcsmenuoperation` VALUES ('17', '设备信息管理', '16', null, 'HHECS.View.EquipmentInfo.WinEquipment', 'menu', '/Content/Icon/菜单.png', 'equipment', '', '0', '2018-10-29 14:22:04', '管理员', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('18', '设备类型管理', '16', null, 'HHECS.View.EquipmentInfo.WinEquipmentType', 'menu', '/Content/Icon/菜单.png', 'equipmentType', '', '0', '2018-10-29 14:22:55', '管理员', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('22', '库位', null, '30', '', 'catalog', '/Content/Icon/目录.png', '', '', '0', '2018-11-13 14:39:43', '管理员', '2018-11-13', '管理员');
INSERT INTO `wcsmenuoperation` VALUES ('23', '托盘管理', '5', '0', 'HHECS.View.ContainerInfo.WinPallet', 'menu', '/Content/Icon/菜单.png', '', '', '0', '2018-11-13 14:41:47', '管理员', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('24', '库位管理', '22', '0', 'HHECS.View.LocationInfo.WinLocation', 'menu', '/Content/Icon/菜单.png', '', '', '0', '2018-11-13 14:42:41', '管理员', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('25', '编辑', '12', '0', '', 'button', '/Content/Icon/按钮.png', 'user:edit', '', '0', '2018-11-22 13:31:29', 'superAdmin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('26', '启用', '12', '0', '', 'button', '/Content/Icon/按钮.png', 'user:enable', '', '0', '2018-11-22 13:31:58', 'superAdmin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('27', '禁用', '12', '0', '', 'button', '/Content/Icon/按钮.png', 'user:disable', '', '0', '2018-11-22 13:32:31', 'superAdmin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('28', '新增', '3', '0', '/', 'button', '/Content/Icon/按钮.png', 'permission:add', '', '0', '2019-08-26 15:03:39', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('29', '删除', '3', '0', '', 'button', '/Content/Icon/按钮.png', 'permission:delete', '', '0', '2019-08-26 15:04:13', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('30', '刷新', '3', '0', '', 'button', '/Content/Icon/按钮.png', 'permission:query', '', '0', '2019-08-26 15:04:29', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('31', '查询', '8', '0', '', 'button', '/Content/Icon/按钮.png', 'config:query', '', '0', '2019-08-26 15:07:42', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('32', '新增', '8', '0', '', 'button', '/Content/Icon/按钮.png', 'config:add', '', '0', '2019-08-26 15:07:57', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('33', '编辑', '8', '0', '', 'button', '/Content/Icon/按钮.png', 'config:edit', '', '0', '2019-08-26 15:08:09', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('34', '删除', '8', '0', '', 'button', '/Content/Icon/按钮.png', 'config:delete', '', '0', '2019-08-26 15:08:26', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('35', '查询', '9', '0', '', 'button', '/Content/Icon/按钮.png', 'dict:query', '', '0', '2019-08-26 15:10:18', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('36', '新增', '9', '0', '', 'button', '/Content/Icon/按钮.png', 'dict:add', '', '0', '2019-08-26 15:10:35', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('37', '编辑', '9', '0', '', 'button', '/Content/Icon/按钮.png', 'dict:edit', '', '0', '2019-08-26 15:10:55', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('38', '删除', '9', '0', '', 'button', '/Content/Icon/按钮.png', 'dict:delete', '', '0', '2019-08-26 15:11:09', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('39', '刷新明细', '9', '0', '', 'button', '/Content/Icon/按钮.png', 'dictdetail:query', '', '0', '2019-08-26 15:11:31', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('40', '新增明细', '9', '0', '', 'button', '/Content/Icon/按钮.png', 'dictdetail:add', '', '0', '2019-08-26 15:11:52', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('41', '编辑明细', '9', '0', '', 'button', '/Content/Icon/按钮.png', 'dictdetail:edit', '', '0', '2019-08-26 15:12:08', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('42', '删除明细', '9', '0', '', 'button', '/Content/Icon/按钮.png', 'dictdetail:delete', '', '0', '2019-08-26 15:12:28', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('43', '新增', '15', '0', '', 'button', '/Content/Icon/按钮.png', 'role:add', '', '0', '2019-08-26 15:16:00', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('44', '编辑', '15', '0', '', 'button', '/Content/Icon/按钮.png', 'role:edit', '', '0', '2019-08-26 15:16:16', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('45', '删除', '15', '0', '', 'button', '/Content/Icon/按钮.png', 'role:delete', '', '0', '2019-08-26 15:16:28', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('46', '刷新', '15', '0', '', 'button', '/Content/Icon/按钮.png', 'role:query', '', '0', '2019-08-26 15:16:45', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('47', '查询', '23', '0', '', 'button', '/Content/Icon/按钮.png', 'container:query', '', '0', '2019-08-26 15:22:47', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('48', '新增', '23', '0', '', 'button', '/Content/Icon/按钮.png', 'container:add', '', '0', '2019-08-26 15:23:03', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('49', '删除', '23', '0', '', 'button', '/Content/Icon/按钮.png', 'container:delete', '', '0', '2019-08-26 15:23:26', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('50', '打印', '23', '0', '', 'button', '/Content/Icon/按钮.png', 'container:print', '', '0', '2019-08-26 15:25:09', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('51', '更改状态', '24', '0', '', 'button', '/Content/Icon/按钮.png', 'location:changestatus', '', '0', '2019-08-26 15:26:17', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('52', '查询', '24', '0', '', 'button', '/Content/Icon/按钮.png', 'location:query', '', '0', '2019-08-26 15:26:38', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('53', '新增', '24', '0', '', 'button', '/Content/Icon/按钮.png', 'location:add', '', '0', '2019-08-26 15:29:30', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('54', '删除', '24', '0', '', 'button', '/Content/Icon/按钮.png', 'location:delete', '', '0', '2019-08-26 15:31:00', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('55', '查询', '6', '0', '', 'button', '/Content/Icon/按钮.png', 'task:query', '', '0', '2019-08-26 15:35:19', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('56', '新增', '6', '0', '', 'button', '/Content/Icon/按钮.png', 'task:add', '', '0', '2019-08-26 15:35:42', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('57', '下发', '6', '0', '', 'button', '/Content/Icon/按钮.png', 'task:assgin', '', '0', '2019-08-26 15:35:59', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('58', '删除', '6', '0', '', 'button', '/Content/Icon/按钮.png', 'task:delete', '', '0', '2019-08-26 15:36:15', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('59', '维护', '6', '0', '', 'button', '/Content/Icon/按钮.png', 'task:edit', '', '0', '2019-08-26 15:36:27', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('60', '查询', '17', '0', '', 'button', '/Content/Icon/按钮.png', 'equipment:query', '', '0', '2019-08-26 15:39:36', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('61', '查看详细', '17', '0', '', 'button', '/Content/Icon/按钮.png', 'equipment:viewdetails', '', '0', '2019-08-26 15:39:52', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('62', '新增设备', '17', '0', '', 'button', '/Content/Icon/按钮.png', 'equipment:add', '', '0', '2019-08-26 15:40:09', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('63', '编辑设备', '17', '0', '', 'button', '/Content/Icon/按钮.png', 'equipment:edit', '', '0', '2019-08-26 15:40:24', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('64', '删除设备', '17', '0', '', 'button', '/Content/Icon/按钮.png', 'equipment:delete', '', '0', '2019-08-26 15:40:38', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('65', '刷新明细', '17', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmentdetail:query', '', '0', '2019-08-26 15:41:01', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('66', '保存明细', '17', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmentdetail:save', '', '0', '2019-08-26 15:41:17', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('67', '删除明细', '17', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmentdetail:delete', '', '0', '2019-08-26 15:41:44', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('68', '同步明细', '17', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmentdetail:sync', '', '0', '2019-08-26 15:42:02', 'admin', '2019-08-26', 'admin');
INSERT INTO `wcsmenuoperation` VALUES ('69', '查询', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttype:query', '', '0', '2019-08-26 15:45:10', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('70', '查看详细', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttype:viewdetails', '', '0', '2019-08-26 15:45:26', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('71', '新增', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttype:add', '', '0', '2019-08-26 15:45:47', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('72', '编辑', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttype:edit', '', '0', '2019-08-26 15:46:06', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('73', '复制', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttype:copy', '', '0', '2019-08-26 15:46:30', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('74', '删除', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttype:delete', '', '0', '2019-08-26 15:46:42', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('75', '刷新明细', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttypedetail:query', '', '0', '2019-08-26 15:47:17', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('76', '新增明细', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttypedetail:add', '', '0', '2019-08-26 15:47:38', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('77', '编辑明细', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttypedetail:edit', '', '0', '2019-08-26 15:47:50', 'admin', null, '');
INSERT INTO `wcsmenuoperation` VALUES ('78', '删除明细', '18', '0', '', 'button', '/Content/Icon/按钮.png', 'equipmenttypedetail:delete', '', '0', '2019-08-26 15:48:05', 'admin', null, '');

-- ----------------------------
-- Table structure for wcsrole
-- ----------------------------
DROP TABLE IF EXISTS `wcsrole`;
CREATE TABLE `wcsrole` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `roleName` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `remark` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `disable` tinyint(4) DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsrole
-- ----------------------------
INSERT INTO `wcsrole` VALUES ('1', 'admin', null, '0', null, null, null, null);
INSERT INTO `wcsrole` VALUES ('2', '操作员', '普通权限', '0', null, null, null, null);

-- ----------------------------
-- Table structure for wcsrolemenuoperation
-- ----------------------------
DROP TABLE IF EXISTS `wcsrolemenuoperation`;
CREATE TABLE `wcsrolemenuoperation` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `roleId` int(11) DEFAULT NULL,
  `menuOperationId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=499 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsrolemenuoperation
-- ----------------------------
INSERT INTO `wcsrolemenuoperation` VALUES ('367', '3', '1');
INSERT INTO `wcsrolemenuoperation` VALUES ('368', '3', '3');
INSERT INTO `wcsrolemenuoperation` VALUES ('369', '3', '4');
INSERT INTO `wcsrolemenuoperation` VALUES ('370', '3', '6');
INSERT INTO `wcsrolemenuoperation` VALUES ('371', '3', '8');
INSERT INTO `wcsrolemenuoperation` VALUES ('372', '3', '9');
INSERT INTO `wcsrolemenuoperation` VALUES ('373', '3', '11');
INSERT INTO `wcsrolemenuoperation` VALUES ('374', '4', '4');
INSERT INTO `wcsrolemenuoperation` VALUES ('375', '4', '6');
INSERT INTO `wcsrolemenuoperation` VALUES ('376', '5', '5');
INSERT INTO `wcsrolemenuoperation` VALUES ('377', '5', '23');
INSERT INTO `wcsrolemenuoperation` VALUES ('378', '6', '10');
INSERT INTO `wcsrolemenuoperation` VALUES ('379', '6', '12');
INSERT INTO `wcsrolemenuoperation` VALUES ('380', '6', '13');
INSERT INTO `wcsrolemenuoperation` VALUES ('381', '6', '14');
INSERT INTO `wcsrolemenuoperation` VALUES ('382', '6', '15');
INSERT INTO `wcsrolemenuoperation` VALUES ('383', '6', '25');
INSERT INTO `wcsrolemenuoperation` VALUES ('384', '6', '26');
INSERT INTO `wcsrolemenuoperation` VALUES ('385', '6', '27');
INSERT INTO `wcsrolemenuoperation` VALUES ('386', '7', '5');
INSERT INTO `wcsrolemenuoperation` VALUES ('387', '7', '10');
INSERT INTO `wcsrolemenuoperation` VALUES ('388', '7', '12');
INSERT INTO `wcsrolemenuoperation` VALUES ('389', '7', '13');
INSERT INTO `wcsrolemenuoperation` VALUES ('390', '7', '14');
INSERT INTO `wcsrolemenuoperation` VALUES ('391', '7', '15');
INSERT INTO `wcsrolemenuoperation` VALUES ('392', '7', '23');
INSERT INTO `wcsrolemenuoperation` VALUES ('393', '7', '25');
INSERT INTO `wcsrolemenuoperation` VALUES ('394', '7', '26');
INSERT INTO `wcsrolemenuoperation` VALUES ('395', '7', '27');
INSERT INTO `wcsrolemenuoperation` VALUES ('396', '8', '1');
INSERT INTO `wcsrolemenuoperation` VALUES ('397', '8', '3');
INSERT INTO `wcsrolemenuoperation` VALUES ('398', '8', '4');
INSERT INTO `wcsrolemenuoperation` VALUES ('399', '8', '5');
INSERT INTO `wcsrolemenuoperation` VALUES ('400', '8', '6');
INSERT INTO `wcsrolemenuoperation` VALUES ('401', '8', '8');
INSERT INTO `wcsrolemenuoperation` VALUES ('402', '8', '9');
INSERT INTO `wcsrolemenuoperation` VALUES ('403', '8', '10');
INSERT INTO `wcsrolemenuoperation` VALUES ('404', '8', '11');
INSERT INTO `wcsrolemenuoperation` VALUES ('405', '8', '12');
INSERT INTO `wcsrolemenuoperation` VALUES ('406', '8', '13');
INSERT INTO `wcsrolemenuoperation` VALUES ('407', '8', '14');
INSERT INTO `wcsrolemenuoperation` VALUES ('408', '8', '15');
INSERT INTO `wcsrolemenuoperation` VALUES ('409', '8', '23');
INSERT INTO `wcsrolemenuoperation` VALUES ('410', '8', '25');
INSERT INTO `wcsrolemenuoperation` VALUES ('411', '8', '26');
INSERT INTO `wcsrolemenuoperation` VALUES ('412', '8', '27');
INSERT INTO `wcsrolemenuoperation` VALUES ('413', '9', '4');
INSERT INTO `wcsrolemenuoperation` VALUES ('414', '9', '5');
INSERT INTO `wcsrolemenuoperation` VALUES ('415', '9', '6');
INSERT INTO `wcsrolemenuoperation` VALUES ('416', '9', '23');
INSERT INTO `wcsrolemenuoperation` VALUES ('417', '1', '1');
INSERT INTO `wcsrolemenuoperation` VALUES ('418', '1', '3');
INSERT INTO `wcsrolemenuoperation` VALUES ('419', '1', '4');
INSERT INTO `wcsrolemenuoperation` VALUES ('420', '1', '5');
INSERT INTO `wcsrolemenuoperation` VALUES ('421', '1', '6');
INSERT INTO `wcsrolemenuoperation` VALUES ('422', '1', '8');
INSERT INTO `wcsrolemenuoperation` VALUES ('423', '1', '9');
INSERT INTO `wcsrolemenuoperation` VALUES ('424', '1', '10');
INSERT INTO `wcsrolemenuoperation` VALUES ('425', '1', '11');
INSERT INTO `wcsrolemenuoperation` VALUES ('426', '1', '12');
INSERT INTO `wcsrolemenuoperation` VALUES ('427', '1', '13');
INSERT INTO `wcsrolemenuoperation` VALUES ('428', '1', '14');
INSERT INTO `wcsrolemenuoperation` VALUES ('429', '1', '15');
INSERT INTO `wcsrolemenuoperation` VALUES ('430', '1', '16');
INSERT INTO `wcsrolemenuoperation` VALUES ('431', '1', '17');
INSERT INTO `wcsrolemenuoperation` VALUES ('432', '1', '18');
INSERT INTO `wcsrolemenuoperation` VALUES ('433', '1', '22');
INSERT INTO `wcsrolemenuoperation` VALUES ('434', '1', '23');
INSERT INTO `wcsrolemenuoperation` VALUES ('435', '1', '24');
INSERT INTO `wcsrolemenuoperation` VALUES ('436', '1', '25');
INSERT INTO `wcsrolemenuoperation` VALUES ('437', '1', '26');
INSERT INTO `wcsrolemenuoperation` VALUES ('438', '1', '27');
INSERT INTO `wcsrolemenuoperation` VALUES ('439', '1', '28');
INSERT INTO `wcsrolemenuoperation` VALUES ('440', '1', '29');
INSERT INTO `wcsrolemenuoperation` VALUES ('441', '1', '30');
INSERT INTO `wcsrolemenuoperation` VALUES ('442', '1', '31');
INSERT INTO `wcsrolemenuoperation` VALUES ('443', '1', '32');
INSERT INTO `wcsrolemenuoperation` VALUES ('444', '1', '33');
INSERT INTO `wcsrolemenuoperation` VALUES ('445', '1', '34');
INSERT INTO `wcsrolemenuoperation` VALUES ('446', '1', '35');
INSERT INTO `wcsrolemenuoperation` VALUES ('447', '1', '36');
INSERT INTO `wcsrolemenuoperation` VALUES ('448', '1', '37');
INSERT INTO `wcsrolemenuoperation` VALUES ('449', '1', '38');
INSERT INTO `wcsrolemenuoperation` VALUES ('450', '1', '39');
INSERT INTO `wcsrolemenuoperation` VALUES ('451', '1', '40');
INSERT INTO `wcsrolemenuoperation` VALUES ('452', '1', '41');
INSERT INTO `wcsrolemenuoperation` VALUES ('453', '1', '42');
INSERT INTO `wcsrolemenuoperation` VALUES ('454', '1', '43');
INSERT INTO `wcsrolemenuoperation` VALUES ('455', '1', '44');
INSERT INTO `wcsrolemenuoperation` VALUES ('456', '1', '45');
INSERT INTO `wcsrolemenuoperation` VALUES ('457', '1', '46');
INSERT INTO `wcsrolemenuoperation` VALUES ('458', '1', '47');
INSERT INTO `wcsrolemenuoperation` VALUES ('459', '1', '48');
INSERT INTO `wcsrolemenuoperation` VALUES ('460', '1', '49');
INSERT INTO `wcsrolemenuoperation` VALUES ('461', '1', '50');
INSERT INTO `wcsrolemenuoperation` VALUES ('462', '1', '51');
INSERT INTO `wcsrolemenuoperation` VALUES ('463', '1', '52');
INSERT INTO `wcsrolemenuoperation` VALUES ('464', '1', '53');
INSERT INTO `wcsrolemenuoperation` VALUES ('465', '1', '54');
INSERT INTO `wcsrolemenuoperation` VALUES ('466', '1', '55');
INSERT INTO `wcsrolemenuoperation` VALUES ('467', '1', '56');
INSERT INTO `wcsrolemenuoperation` VALUES ('468', '1', '57');
INSERT INTO `wcsrolemenuoperation` VALUES ('469', '1', '58');
INSERT INTO `wcsrolemenuoperation` VALUES ('470', '1', '59');
INSERT INTO `wcsrolemenuoperation` VALUES ('471', '1', '60');
INSERT INTO `wcsrolemenuoperation` VALUES ('472', '1', '61');
INSERT INTO `wcsrolemenuoperation` VALUES ('473', '1', '62');
INSERT INTO `wcsrolemenuoperation` VALUES ('474', '1', '63');
INSERT INTO `wcsrolemenuoperation` VALUES ('475', '1', '64');
INSERT INTO `wcsrolemenuoperation` VALUES ('476', '1', '65');
INSERT INTO `wcsrolemenuoperation` VALUES ('477', '1', '66');
INSERT INTO `wcsrolemenuoperation` VALUES ('478', '1', '67');
INSERT INTO `wcsrolemenuoperation` VALUES ('479', '1', '68');
INSERT INTO `wcsrolemenuoperation` VALUES ('480', '1', '69');
INSERT INTO `wcsrolemenuoperation` VALUES ('481', '1', '70');
INSERT INTO `wcsrolemenuoperation` VALUES ('482', '1', '71');
INSERT INTO `wcsrolemenuoperation` VALUES ('483', '1', '72');
INSERT INTO `wcsrolemenuoperation` VALUES ('484', '1', '73');
INSERT INTO `wcsrolemenuoperation` VALUES ('485', '1', '74');
INSERT INTO `wcsrolemenuoperation` VALUES ('486', '1', '75');
INSERT INTO `wcsrolemenuoperation` VALUES ('487', '1', '76');
INSERT INTO `wcsrolemenuoperation` VALUES ('488', '1', '77');
INSERT INTO `wcsrolemenuoperation` VALUES ('489', '1', '78');
INSERT INTO `wcsrolemenuoperation` VALUES ('490', '2', '1');
INSERT INTO `wcsrolemenuoperation` VALUES ('491', '2', '4');
INSERT INTO `wcsrolemenuoperation` VALUES ('492', '2', '5');
INSERT INTO `wcsrolemenuoperation` VALUES ('493', '2', '6');
INSERT INTO `wcsrolemenuoperation` VALUES ('494', '2', '8');
INSERT INTO `wcsrolemenuoperation` VALUES ('495', '2', '9');
INSERT INTO `wcsrolemenuoperation` VALUES ('496', '2', '22');
INSERT INTO `wcsrolemenuoperation` VALUES ('497', '2', '23');
INSERT INTO `wcsrolemenuoperation` VALUES ('498', '2', '24');

-- ----------------------------
-- Table structure for wcstask
-- ----------------------------
DROP TABLE IF EXISTS `wcstask`;
CREATE TABLE `wcstask` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `preTaskId` int(11) DEFAULT '0' COMMENT '前置任务',
  `remoteTaskNo` varchar(50) NOT NULL DEFAULT '0',
  `priority` smallint(6) NOT NULL DEFAULT '10' COMMENT '优先级(1-99)',
  `taskType` smallint(6) NOT NULL COMMENT '任务类型',
  `taskStatus` smallint(6) NOT NULL DEFAULT '1' COMMENT '首状态',
  `containerCode` varchar(50) DEFAULT NULL COMMENT '容器编号',
  `port` int(11) DEFAULT NULL COMMENT '出入口',
  `gateway` int(11) DEFAULT NULL COMMENT '实时位置',
  `roadway` int(11) DEFAULT NULL COMMENT '巷道',
  `reqLength` varchar(20) DEFAULT NULL COMMENT '长度',
  `reqWeight` varchar(20) DEFAULT NULL COMMENT '重量',
  `reqWidth` varchar(20) DEFAULT NULL COMMENT '宽度',
  `reqHeight` varchar(20) DEFAULT NULL COMMENT '高度',
  `fromLocationCode` varchar(50) DEFAULT NULL COMMENT '源库位',
  `toLocationCode` varchar(50) DEFAULT NULL COMMENT '目的库位',
  `platform` varchar(50) DEFAULT NULL COMMENT '平台',
  `stage` int(11) NOT NULL DEFAULT '1',
  `isEmptyOut` int(11) NOT NULL DEFAULT '0' COMMENT '新增、是否空出标记',
  `isDoubleIn` int(11) NOT NULL DEFAULT '0' COMMENT '新增、是否重入处理标志',
  `isForkError` int(11) NOT NULL DEFAULT '0' COMMENT '货叉取货错误',
  `doubleInLocationCode` varchar(50) DEFAULT NULL COMMENT '重入时，重新写入的去向地址',
  `sendAgain` int(11) DEFAULT '0' COMMENT '重新下发给堆垛机，1表示重新下发，2表示已经响应重新下发',
  `warehouseCode` varchar(50) DEFAULT NULL COMMENT '仓库',
  `created` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `createdBy` varchar(50) NOT NULL COMMENT '任务下达人',
  `updated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最后修改时间',
  `updatedBy` varchar(50) DEFAULT NULL COMMENT '更新用户',
  `deleted` bit(1) NOT NULL DEFAULT b'0' COMMENT '删除标记',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`) USING BTREE,
  KEY `code` (`remoteTaskNo`,`deleted`) USING BTREE,
  KEY `status` (`taskStatus`,`warehouseCode`,`deleted`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1740 DEFAULT CHARSET=utf8mb4 COMMENT='立库任务表';

-- ----------------------------
-- Records of wcstask
-- ----------------------------
INSERT INTO `wcstask` VALUES ('1387', '0', '0', '5', '100', '100', 'M00382', '0', null, null, null, null, null, null, '', 'L04-08-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 18:43:28', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1388', '0', '0', '5', '100', '100', 'M00405', '0', null, null, null, null, null, null, '', 'L04-06-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 18:43:35', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1389', '0', '0', '5', '100', '100', 'M00404', '0', null, null, null, null, null, null, '', 'L04-06-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 18:43:38', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1390', '0', '0', '10', '100', '100', 'M00001', '1006', null, null, null, null, null, null, '', 'L01-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 18:44:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1392', '0', '0', '10', '700', '100', 'M00001', '1001', null, null, null, null, null, null, 'L01-14-01', 'L01-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 18:53:53', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1393', '0', '0', '10', '600', '100', 'M00001', '1001', null, null, null, null, null, null, 'L01-14-01', null, null, '1', '1', '0', '0', null, '0', 'XT0001', '2018-12-11 19:07:22', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1394', '0', '0', '10', '300', '100', 'M00404', '1012', null, null, null, null, null, null, 'L04-06-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:20:03', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1395', '0', '0', '10', '300', '100', 'M00382', '1012', null, null, null, null, null, null, 'L04-08-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:20:05', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1396', '0', '0', '10', '100', '100', 'M00001', '1006', null, null, null, null, null, null, '', 'L01-01-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:24:43', 'youjie', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1397', '0', '0', '5', '100', '100', 'M00399', '0', null, null, null, null, null, null, '', 'L04-06-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:56', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1398', '0', '0', '5', '100', '100', 'M00398', '0', null, null, null, null, null, null, '', 'L04-06-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:57', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1399', '0', '0', '5', '100', '100', 'M00397', '0', null, null, null, null, null, null, '', 'L04-06-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:58', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1400', '0', '0', '5', '100', '100', 'M00395', '0', null, null, null, null, null, null, '', 'L04-05-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:58', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1401', '0', '0', '5', '100', '100', 'M00394', '0', null, null, null, null, null, null, '', 'L04-10-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:59', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1402', '0', '0', '5', '100', '100', 'M00393', '0', null, null, null, null, null, null, '', 'L04-10-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:26:00', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1403', '0', '0', '5', '100', '100', 'M00392', '0', null, null, null, null, null, null, '', 'L04-10-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:26:02', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1404', '0', '0', '5', '100', '100', 'M00390', '0', null, null, null, null, null, null, '', 'L04-10-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:26:03', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1405', '0', '0', '5', '100', '100', 'M00391', '0', null, null, null, null, null, null, '', 'L04-10-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:26:04', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1407', '0', '0', '10', '700', '100', 'M00002', '1001', null, null, null, null, null, null, 'L01-01-02', 'L01-01-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-11 19:39:44', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1408', '0', '0', '10', '700', '100', 'M00391', '1012', null, null, null, null, null, null, 'L04-10-02', 'L04-10-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 08:46:33', 'xqs', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1409', '0', '0', '10', '300', '100', 'M00390', '1012', null, null, null, null, null, null, 'L04-10-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 08:48:57', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1410', '0', '0', '10', '300', '100', 'M00405', '1012', null, null, null, null, null, null, 'L04-06-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 08:48:58', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1411', '0', '0', '10', '300', '100', 'M00395', '1012', null, null, null, null, null, null, 'L04-05-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 08:48:59', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1412', '0', '0', '10', '300', '100', 'M00399', '1012', null, null, null, null, null, null, 'L04-06-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 08:49:00', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1413', '0', '0', '10', '300', '100', 'M00398', '1012', null, null, null, null, null, null, 'L04-06-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 08:49:02', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1414', '0', '0', '5', '100', '100', 'M00370', '0', null, null, null, null, null, null, '', 'L04-13-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:32:01', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1415', '0', '0', '5', '100', '10', 'M00368', '0', null, null, null, null, null, null, '', 'L04-13-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:32:02', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1416', '0', '0', '10', '100', '100', 'M00003', '1006', null, null, null, null, null, null, '', 'L04-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:38:11', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1417', '0', '0', '5', '100', '100', 'M00405', '0', null, null, null, null, null, null, '', 'L04-14-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:33', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1418', '0', '0', '5', '100', '100', 'M00404', '0', null, null, null, null, null, null, '', 'L04-14-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:33', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1419', '0', '0', '5', '100', '100', 'M00403', '0', null, null, null, null, null, null, '', 'L04-14-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:33', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1420', '0', '0', '5', '200', '100', 'M00003', '0', null, null, null, null, null, null, 'L04-14-01', 'L04-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:54', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1421', '0', '0', '5', '100', '100', 'M00367', '0', null, null, null, null, null, null, '', 'L04-13-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:56', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1422', '0', '0', '5', '100', '100', 'M00369', '0', null, null, null, null, null, null, '', 'L04-13-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:57', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1423', '0', '0', '10', '300', '100', 'M00393', '1006', null, null, null, null, null, null, 'L04-10-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:46:45', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1424', '0', '0', '10', '300', '100', 'M00397', '1001', null, null, null, null, null, null, 'L04-06-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 09:46:46', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1425', '0', '0', '5', '500', '100', 'S00020', '1012', null, null, null, null, null, null, null, 'L01-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 10:12:46', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1426', '0', '0', '5', '500', '100', 'S00021', '1012', null, null, null, null, null, null, null, 'L01-14-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 10:16:43', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1427', '0', '0', '10', '600', '100', 'S00020', '1012', null, null, null, null, null, null, 'L01-14-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 10:19:49', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1428', '0', '0', '10', '600', '100', 'S00021', '1012', null, null, null, null, null, null, 'L01-14-02', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 10:25:02', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1429', '0', '0', '5', '500', '100', 'M00359', '1012', null, null, null, null, null, null, null, 'L01-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 10:26:43', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1430', '0', '0', '10', '300', '100', 'M00370', '1006', null, null, null, null, null, null, 'L04-13-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 10:30:05', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1432', '0', '0', '5', '500', '100', 'M00339', '1012', null, null, null, null, null, null, null, 'L04-12-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 10:38:53', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1433', '0', '0', '5', '500', '10', 'M00335', '1012', null, null, null, null, null, null, null, 'L01-10-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 10:39:20', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1434', '0', '0', '5', '500', '100', 'S00022', '1012', null, null, null, null, null, null, null, 'L04-13-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 10:51:51', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1435', '0', '0', '5', '500', '100', 'S00019', '1012', null, null, null, null, null, null, null, 'L01-14-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 11:09:37', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1436', '0', '0', '5', '500', '100', 'S00020', '1012', null, null, null, null, null, null, null, 'L04-08-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 11:10:33', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1437', '0', '0', '10', '600', '10', 'M00359', '1012', null, null, null, null, null, null, 'L01-14-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 11:13:33', 'wjj', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1438', '0', '0', '5', '200', '10', 'M00391', '0', null, null, null, null, null, null, 'L04-10-02', 'L04-10-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 12:06:42', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1439', '0', '0', '5', '100', '100', 'M00402', '0', null, null, null, null, null, null, '', 'L04-10-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 12:06:43', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1440', '0', '0', '10', '300', '100', 'M00001', '1012', null, null, null, null, null, null, 'L01-01-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 13:16:02', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1441', '0', '0', '10', '300', '100', 'M00003', '1001', null, null, null, null, null, null, 'L04-14-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 13:37:12', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1442', '0', '0', '10', '100', '100', 'M00005', '1006', null, null, null, null, null, null, '', 'L01-01-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 13:38:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1443', '0', '0', '10', '300', '100', 'M00005', '1012', null, null, null, null, null, null, 'L01-01-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 13:39:40', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1444', '0', '0', '10', '100', '100', 'M00010', '1006', null, null, null, null, null, null, '', 'L02-01-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 13:47:13', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1445', '0', '0', '10', '300', '100', 'M00010', '1012', null, null, null, null, null, null, 'L02-01-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 13:48:12', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1446', '0', '0', '10', '300', '100', 'M00369', '1006', null, null, null, null, null, null, 'L04-13-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:05:38', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1447', '0', '0', '10', '300', '100', 'M00367', '1006', null, null, null, null, null, null, 'L04-13-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:05:39', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1448', '0', '0', '10', '600', '100', 'M00002', '1012', null, null, null, null, null, null, 'L01-01-02', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:10:37', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1449', '0', '0', '10', '100', '100', 'M00001', '1006', null, null, null, null, null, null, '', 'L01-01-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:23:14', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1450', '0', '0', '10', '300', '100', 'M00001', '1012', null, null, null, null, null, null, 'L01-01-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:30:06', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1451', '0', '0', '10', '700', '100', 'M00002', '1012', null, null, null, null, null, null, 'L01-01-02', 'L01-01-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:39:24', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1452', '0', '0', '5', '100', '100', 'M00378', '0', null, null, null, null, null, null, '', 'L04-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:41:54', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1453', '0', '0', '5', '100', '100', 'L00320', '0', null, null, null, null, null, null, '', 'L04-13-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:41:55', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1454', '0', '0', '5', '100', '100', 'S00021', '0', null, null, null, null, null, null, '', 'L04-13-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:41:56', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1455', '0', '0', '10', '100', '100', 'M00003', '1006', null, null, null, null, null, null, '', 'L01-01-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:43:22', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1456', '0', '0', '10', '700', '100', 'M00003', '1012', null, null, null, null, null, null, 'L01-01-03', 'L01-01-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:44:11', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1457', '0', '0', '10', '600', '100', 'M00003', '1012', null, null, null, null, null, null, 'L01-01-03', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:45:36', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1458', '0', '0', '10', '100', '100', 'M00003', '1006', null, null, null, null, null, null, '', 'L01-01-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:46:34', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1459', '0', '0', '10', '300', '100', 'M00003', '1012', null, null, null, null, null, null, 'L01-01-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 14:48:23', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1466', '0', '0', '10', '300', '100', 'M00378', '1012', null, null, null, null, null, null, 'L04-14-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:12:02', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1467', '0', '0', '10', '300', '100', 'L00320', '1012', null, null, null, null, null, null, 'L04-13-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:12:03', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1468', '0', '0', '10', '300', '100', 'S00021', '1012', null, null, null, null, null, null, 'L04-13-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:12:03', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1469', '0', '0', '10', '900', '100', 'S00022', '1012', null, null, null, null, null, null, 'L04-13-03', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:15:31', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1470', '0', '0', '5', '100', '100', 'M00367', '0', null, null, null, null, null, null, '', 'L04-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:18:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1471', '0', '0', '5', '100', '100', 'M00374', '0', null, null, null, null, null, null, '', 'L01-14-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:22:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1472', '0', '0', '5', '100', '100', 'M00373', '0', null, null, null, null, null, null, '', 'L01-14-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:22:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1473', '0', '0', '5', '100', '100', 'M00372', '0', null, null, null, null, null, null, '', 'L01-14-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:22:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1474', '0', '0', '5', '100', '100', 'M00369', '0', null, null, null, null, null, null, '', 'L04-13-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:22:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1475', '0', '0', '10', '700', '100', 'M00374', '1012', null, null, null, null, null, null, 'L01-14-03', 'L01-14-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:23:39', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1476', '0', '0', '10', '100', '100', 'M00004', '0', null, null, null, null, null, null, '', 'L04-13-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:24:03', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1477', '0', '0', '10', '300', '100', 'M00004', '1012', null, null, null, null, null, null, 'L04-13-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:24:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1478', '0', '0', '10', '600', '100', 'M00374', '1012', null, null, null, null, null, null, 'L01-14-03', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:25:29', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1479', '0', '0', '10', '300', '10', 'M00369', '1006', null, null, null, null, null, null, 'L04-13-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 15:28:58', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1480', '0', '0', '10', '100', '100', 'M00003', '0', null, null, null, null, null, null, '', 'L02-01-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 18:46:42', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1481', '0', '0', '10', '300', '100', 'M00002', '1012', null, null, null, null, null, null, 'L01-01-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 18:57:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1482', '0', '0', '15', '800', '100', 'M00003', '1012', null, null, null, null, null, null, 'L02-01-02', 'L04-01-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 18:57:56', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1484', '0', '0', '10', '700', '100', 'M00003', '1012', null, null, null, null, null, null, 'L04-01-01', 'L04-01-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 19:11:24', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1485', '0', '0', '10', '600', '100', 'M00003', '1012', null, null, null, null, null, null, 'L04-01-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 19:14:04', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1486', '0', '0', '5', '100', '100', 'M00377', '0', null, null, null, null, null, null, '', 'L03-14-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 19:17:53', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1487', '0', '0', '10', '300', '10', 'M00404', '1012', null, null, null, null, null, null, 'L04-14-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 19:20:04', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1488', '0', '0', '10', '100', '100', 'M00001', '0', null, null, null, null, null, null, '', 'L01-01-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 19:20:18', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1489', '0', '0', '15', '800', '100', 'M00377', '1012', null, null, null, null, null, null, 'L03-14-03', 'L02-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 19:20:35', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask` VALUES ('1490', '0', '0', '10', '100', '100', 'M00002', '0', null, null, null, null, null, null, '', 'L02-01-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 19:20:51', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1491', '0', '0', '10', '300', '100', 'M00001', '1012', null, null, null, null, null, null, 'L01-01-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 19:41:53', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1492', '0', '0', '15', '800', '100', 'M00002', '1012', null, null, null, null, null, null, 'L02-01-01', 'L04-01-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-12 19:41:54', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1493', '0', '0', '5', '100', '100', 'S00018', '0', null, null, null, null, null, null, '', 'L04-08-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 08:48:06', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1494', '0', '0', '5', '100', '100', 'L00320', '0', null, null, null, null, null, null, '', 'L04-07-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 08:48:07', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1495', '0', '0', '5', '100', '100', 'M00379', '0', null, null, null, null, null, null, '', 'L04-07-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:25:14', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1496', '0', '0', '5', '100', '100', 'M00378', '0', null, null, null, null, null, null, '', 'L04-07-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:25:15', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1497', '0', '0', '5', '100', '100', 'M00376', '0', null, null, null, null, null, null, '', 'L04-07-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:25:16', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1498', '0', '0', '5', '100', '100', 'S00021', '0', null, null, null, null, null, null, '', 'L04-13-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:25:16', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1499', '0', '0', '5', '100', '100', 'M00380', '0', null, null, null, null, null, null, '', 'L04-07-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:29:29', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1500', '0', '0', '10', '300', '100', 'M00376', '1006', null, null, null, null, null, null, 'L04-07-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:36:09', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1501', '0', '0', '10', '300', '100', 'M00378', '1006', null, null, null, null, null, null, 'L04-07-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:36:10', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1502', '0', '0', '10', '300', '100', 'M00379', '1006', null, null, null, null, null, null, 'L04-07-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:36:11', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1503', '0', '0', '10', '300', '100', 'M00380', '1006', null, null, null, null, null, null, 'L04-07-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:36:11', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1504', '0', '0', '10', '300', '100', 'M00373', '1012', null, null, null, null, null, null, 'L01-14-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:45:30', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1505', '0', '0', '5', '500', '100', 'M00374', '1012', null, null, null, null, null, null, null, 'L01-14-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:49:40', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1506', '0', '0', '5', '500', '100', 'M00375', '1012', null, null, null, null, null, null, null, 'L04-08-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:51:47', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1507', '0', '0', '10', '600', '100', 'S00019', '1012', null, null, null, null, null, null, 'L01-14-02', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 09:53:18', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1508', '0', '0', '10', '700', '100', 'M00379', '1012', null, null, null, null, null, null, 'L04-09-02', 'L04-09-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 10:00:34', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1509', '0', '0', '10', '700', '100', 'M00380', '1012', null, null, null, null, null, null, 'L04-09-03', 'L04-09-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 10:05:38', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1510', '0', '0', '10', '700', '100', 'M00386', '1012', null, null, null, null, null, null, 'L04-08-04', 'L04-08-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 10:14:21', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1511', '0', '0', '10', '900', '100', 'M00002', '1012', null, null, null, null, null, null, 'L04-01-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 10:19:27', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1512', '0', '0', '10', '700', '100', 'M00002', '1012', null, null, null, null, null, null, 'L04-01-01', 'L04-01-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 10:25:02', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1513', '0', '0', '10', '700', '100', 'M00395', '1012', null, null, null, null, null, null, 'L04-02-05', 'L04-02-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-13 11:34:18', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1514', '0', '0', '5', '100', '100', 'S00015', '0', null, null, null, null, null, null, '', 'L04-09-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-14 08:57:09', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1515', '0', '0', '5', '100', '100', 'S00019', '0', null, null, null, null, null, null, '', 'L04-09-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-14 08:57:10', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1516', '0', '0', '10', '700', '100', 'S00021', '1012', null, null, null, null, null, null, 'L04-13-05', 'L04-13-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 15:39:47', 'xqs', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1517', '0', '0', '10', '100', '100', 'M00015', '1006', null, null, null, null, null, null, '', 'L01-14-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 17:04:46', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1518', '0', '0', '10', '100', '100', 'M00016', '1006', null, null, null, null, null, null, '', 'L01-14-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 17:09:22', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1519', '0', '0', '10', '100', '100', 'M00018', '1006', null, null, null, null, null, null, '', 'L04-11-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 17:12:13', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1520', '0', '0', '10', '100', '100', 'M00020', '1006', null, null, null, null, null, null, '', 'L01-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 17:18:36', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1521', '0', '0', '10', '100', '100', 'M00030', '1006', null, null, null, null, null, null, '', 'L04-11-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 17:20:38', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1522', '0', '0', '10', '100', '100', 'M00031', '1006', null, null, null, null, null, null, '', 'L04-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 17:21:38', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1523', '0', '0', '10', '100', '100', 'M00110', '1006', null, null, null, null, null, null, '', 'L01-12-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 19:12:18', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1524', '0', '0', '10', '100', '100', 'M00111', '1006', null, null, null, null, null, null, '', 'L01-12-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 19:19:12', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1525', '0', '0', '10', '100', '100', 'M00123', '1006', null, null, null, null, null, null, '', 'L01-12-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 19:22:29', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1526', '0', '0', '10', '100', '100', 'M00060', '1006', null, null, null, null, null, null, '', 'L01-14-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 19:24:10', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1527', '0', '0', '5', '100', '100', 'M00200', '0', null, null, null, null, null, null, '', 'L03-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-17 19:49:51', 'abc', '2019-08-15 17:20:08', 'abc', '\0');
INSERT INTO `wcstask` VALUES ('1528', '0', '0', '10', '100', '100', 'M00019', '1006', null, null, null, null, null, null, '', 'L01-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 08:54:27', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1529', '0', '0', '10', '100', '100', 'M00004', '1006', null, null, null, null, null, null, '', 'L01-12-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 09:02:41', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1530', '0', '0', '5', '100', '100', 'M00382', '0', null, null, null, null, null, null, '', 'L01-13-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 11:47:21', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask` VALUES ('1531', '0', '0', '5', '100', '100', 'M00385', '0', null, null, null, null, null, null, '', 'L04-07-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 11:47:23', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask` VALUES ('1532', '0', '0', '0', '100', '100', 'M00002', '1012', null, null, null, null, null, null, '', 'L02-09-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 14:56:59', 'admin', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1533', '0', '0', '10', '300', '100', 'M00382', '1006', null, null, null, null, null, null, 'L01-13-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 16:34:17', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask` VALUES ('1534', '0', '0', '10', '300', '100', 'M00385', '1006', null, null, null, null, null, null, 'L04-07-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 16:34:20', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask` VALUES ('1535', '0', '0', '10', '700', '100', 'S00015', '1012', null, null, null, null, null, null, 'L04-09-04', 'L04-09-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 16:59:54', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask` VALUES ('1536', '0', '0', '10', '700', '100', 'M00397', '1012', null, null, null, null, null, null, 'L04-07-04', 'L04-07-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 17:05:50', 'my test', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1537', '0', '0', '5', '500', '100', 'M00398', '1012', null, null, null, null, null, null, null, 'L04-07-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 17:21:29', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask` VALUES ('1538', '0', '0', '10', '600', '100', 'S00020', '1012', null, null, null, null, null, null, 'L04-08-03', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-18 17:24:16', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask` VALUES ('1539', '0', '0', '10', '600', '100', 'M00398', '1012', null, null, null, null, null, null, 'L04-07-02', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-19 09:57:29', 'my test', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1540', '0', '0', '5', '500', '100', 'S00020', '1012', null, null, null, null, null, null, null, 'L01-13-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-19 10:07:53', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask` VALUES ('1541', '0', '0', '10', '700', '100', 'S00019', '1012', null, null, null, null, null, null, 'L04-09-05', 'L04-09-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-21 11:04:14', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1542', '0', '0', '10', '700', '100', 'S00015', '1012', null, null, null, null, null, null, 'L04-09-04', 'L04-09-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-21 11:05:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1543', '0', '0', '10', '700', '100', 'M00015', '1012', null, null, null, null, null, null, 'L01-14-02', 'L01-14-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-21 11:09:01', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1544', '0', '0', '10', '700', '100', 'M00060', '1012', null, null, null, null, null, null, 'L01-14-04', 'L01-14-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-21 11:09:01', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1545', '0', '0', '10', '700', '100', 'M00019', '1012', null, null, null, null, null, null, 'L01-11-01', 'L01-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-21 11:09:01', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1546', '0', '0', '10', '700', '100', 'M00200', '1012', null, null, null, null, null, null, 'L03-14-01', 'L03-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-21 15:48:40', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1547', '0', '0', '10', '700', '100', 'M00019', '1012', null, null, null, null, null, null, 'L01-11-01', 'L01-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-21 15:49:09', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1548', '0', '0', '5', '100', '100', 'M00356', '0', null, null, null, null, null, null, '', 'L01-13-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1549', '0', '0', '5', '100', '100', 'M00355', '0', null, null, null, null, null, null, '', 'L01-13-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1550', '0', '0', '5', '100', '100', 'M00354', '0', null, null, null, null, null, null, '', 'L01-13-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1551', '0', '0', '5', '100', '100', 'M00353', '0', null, null, null, null, null, null, '', 'L01-13-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1552', '0', '0', '5', '100', '100', 'M00352', '0', null, null, null, null, null, null, '', 'L04-12-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1553', '0', '0', '5', '100', '100', 'M00351', '0', null, null, null, null, null, null, '', 'L04-12-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:58', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1554', '0', '0', '5', '100', '100', 'M00384', '0', null, null, null, null, null, null, '', 'L04-12-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:58', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1555', '0', '0', '5', '500', '100', 'M00005', '1012', null, null, null, null, null, null, null, 'L01-12-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 09:12:29', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1556', '0', '0', '5', '500', '100', 'M00006', '1012', null, null, null, null, null, null, null, 'L01-01-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 09:16:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1557', '0', '0', '10', '700', '100', 'M00352', '1006', null, null, null, null, null, null, 'L04-12-03', 'L04-12-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 09:42:07', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1558', '0', '0', '10', '700', '100', 'M00351', '1006', null, null, null, null, null, null, 'L04-12-04', 'L04-12-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 09:42:07', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1559', '0', '0', '10', '700', '100', 'M00384', '1006', null, null, null, null, null, null, 'L04-12-05', 'L04-12-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 09:42:07', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1560', '0', '0', '10', '600', '100', 'M00005', '1012', null, null, null, null, null, null, 'L01-12-02', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 09:43:32', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1561', '0', '0', '10', '600', '100', 'M00006', '1012', null, null, null, null, null, null, 'L01-01-05', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 09:50:44', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1562', '0', '0', '5', '500', '100', 'M00005', '1012', null, null, null, null, null, null, null, 'L01-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 10:10:56', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1563', '0', '0', '5', '500', '100', 'M00006', '1012', null, null, null, null, null, null, null, 'L01-12-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 11:01:52', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1564', '0', '0', '5', '500', '100', 'M00007', '1012', null, null, null, null, null, null, null, 'L01-12-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 11:02:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1565', '0', '0', '10', '600', '100', 'M00005', '1012', null, null, null, null, null, null, 'L01-12-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 11:06:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1566', '0', '0', '5', '500', '100', 'M00005', '1012', null, null, null, null, null, null, null, 'L01-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 11:16:58', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1567', '0', '0', '10', '600', '100', 'M00005', '1012', null, null, null, null, null, null, 'L01-12-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 11:24:54', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1568', '0', '0', '5', '500', '100', 'M00005', '1012', null, null, null, null, null, null, null, 'L01-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 11:25:25', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1569', '0', '0', '10', '600', '100', 'M00008', '1012', null, null, null, null, null, null, 'L01-12-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 11:26:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1570', '0', '0', '10', '600', '100', 'M00005', '1012', null, null, null, null, null, null, 'L01-12-02', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 11:52:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1571', '0', '0', '10', '600', '100', 'M00005', '1012', null, null, null, null, null, null, 'L01-12-03', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 11:58:26', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1572', '0', '0', '5', '500', '100', 'M00005', '1012', null, null, null, null, null, null, null, 'L01-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 12:02:29', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1573', '0', '0', '10', '600', '100', 'M00005', '1012', null, null, null, null, null, null, 'L01-12-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 12:10:14', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1574', '0', '0', '10', '700', '100', 'M00356', '1006', null, null, null, null, null, null, 'L01-13-02', 'L01-13-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 13:26:19', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1575', '0', '0', '10', '700', '100', 'M00354', '1006', null, null, null, null, null, null, 'L01-13-04', 'L01-13-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 13:26:19', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1576', '0', '0', '10', '700', '100', 'M00351', '1006', null, null, null, null, null, null, 'L04-12-04', 'L04-12-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 13:26:19', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1577', '0', '0', '5', '500', '100', 'M00005', '1012', null, null, null, null, null, null, null, 'L01-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:33:06', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1578', '0', '0', '5', '500', '100', 'M00006', '1012', null, null, null, null, null, null, null, 'L01-12-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:33:39', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1579', '0', '0', '5', '500', '100', 'M00007', '1012', null, null, null, null, null, null, null, 'L01-12-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:33:45', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1580', '0', '0', '5', '500', '100', 'M00008', '1012', null, null, null, null, null, null, null, 'L01-12-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:33:50', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1581', '0', '0', '10', '600', '100', 'M00005', '1012', null, null, null, null, null, null, 'L01-12-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:35:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1582', '0', '0', '10', '600', '100', 'M00007', '1012', null, null, null, null, null, null, 'L01-12-03', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:36:36', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1583', '0', '0', '10', '600', '100', 'M00006', '1012', null, null, null, null, null, null, 'L01-12-02', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:36:45', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1584', '0', '0', '10', '600', '100', 'M00008', '1012', null, null, null, null, null, null, 'L01-12-04', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:36:54', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1585', '0', '0', '5', '500', '100', 'M00005', '1012', null, null, null, null, null, null, null, 'L01-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:49:04', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1586', '0', '0', '10', '600', '100', 'M00005', '1012', null, null, null, null, null, null, 'L01-12-01', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 14:50:50', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1588', '0', '0', '0', '800', '100', 'M00367', '1012', null, null, null, null, null, null, 'L04-14-01', 'L04-11-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 18:24:25', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1589', '0', '0', '0', '800', '100', 'S00022', '1012', null, null, null, null, null, null, 'L04-13-03', 'L04-11-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 18:24:25', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1590', '0', '0', '0', '800', '100', 'M00367', '1012', null, null, null, null, null, null, 'L04-11-05', 'L04-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 18:43:25', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1591', '0', '0', '0', '800', '100', 'S00022', '1012', null, null, null, null, null, null, 'L04-11-04', 'L04-13-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 18:43:26', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1592', '0', '0', '0', '800', '100', 'M00367', '1012', null, null, null, null, null, null, 'L04-14-01', 'L04-11-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 18:52:23', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1593', '0', '0', '0', '800', '100', 'S00022', '1012', null, null, null, null, null, null, 'L04-13-03', 'L04-11-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 18:52:23', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1594', '0', '0', '0', '800', '100', 'M00367', '1012', null, null, null, null, null, null, 'L04-11-05', 'L04-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 19:32:00', 'superAdmin', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1595', '0', '0', '0', '800', '100', 'S00022', '1012', null, null, null, null, null, null, 'L04-11-04', 'L04-13-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-24 19:32:01', 'superAdmin', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1596', '0', '0', '10', '700', '100', 'M00355', '1006', null, null, null, null, null, null, 'L01-13-03', 'L01-13-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 10:52:08', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1597', '0', '0', '10', '700', '100', 'M00351', '1006', null, null, null, null, null, null, 'L04-12-04', 'L04-12-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 10:52:08', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1598', '0', '0', '10', '700', '100', 'M00384', '1006', null, null, null, null, null, null, 'L04-12-05', 'L04-12-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 10:52:08', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1599', '0', '0', '0', '800', '100', 'M00367', '1006', null, null, null, null, null, null, 'L04-14-01', 'L04-11-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 10:58:00', 'superAdmin', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1600', '0', '0', '0', '800', '100', 'S00022', '1006', null, null, null, null, null, null, 'L04-13-03', 'L04-11-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 10:58:00', 'superAdmin', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1601', '0', '0', '10', '700', '100', 'M00384', '1006', null, null, null, null, null, null, 'L04-12-05', 'L04-12-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 11:17:42', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1602', '0', '0', '10', '700', '100', 'M00382', '1006', null, null, null, null, null, null, 'L04-07-03', 'L04-07-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 11:17:42', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1603', '0', '0', '10', '700', '100', 'M00382', '1006', null, null, null, null, null, null, 'L04-07-03', 'L04-07-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 11:31:20', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1604', '0', '0', '10', '700', '100', 'M00385', '1006', null, null, null, null, null, null, 'L04-06-05', 'L04-06-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 11:31:20', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1605', '0', '0', '10', '700', '100', 'M00387', '1006', null, null, null, null, null, null, 'L04-06-04', 'L04-06-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 11:49:38', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1606', '0', '0', '10', '700', '100', 'M00385', '1006', null, null, null, null, null, null, 'L04-06-05', 'L04-06-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 13:15:56', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask` VALUES ('1607', '0', '0', '0', '800', '100', 'M00367', '1006', null, null, null, null, null, null, 'L04-11-05', 'L04-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 14:51:50', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1608', '0', '0', '0', '800', '100', 'S00022', '1006', null, null, null, null, null, null, 'L04-11-04', 'L04-13-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 14:51:50', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1609', '0', '0', '5', '100', '100', 'M00401', '0', null, null, null, null, null, null, '', 'L04-06-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 15:15:11', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1610', '0', '0', '5', '100', '100', 'M00400', '0', null, null, null, null, null, null, '', 'L04-06-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 15:15:13', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1611', '0', '0', '10', '300', '100', 'M00400', '1012', null, null, null, null, null, null, 'L04-06-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 15:22:10', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1612', '0', '0', '10', '300', '100', 'M00377', '1012', null, null, null, null, null, null, 'L02-14-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 15:22:11', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1613', '0', '0', '10', '300', '100', 'M00372', '1012', null, null, null, null, null, null, 'L01-14-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 15:27:37', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1614', '0', '0', '10', '700', '100', 'M00387', '1006', null, null, null, null, null, null, 'L04-06-04', 'L04-06-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 15:43:32', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1615', '0', '0', '10', '700', '100', 'M00071', '1006', null, null, null, null, null, null, 'L04-06-01', 'L04-06-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 16:07:55', 'wjj', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1616', '0', '0', '0', '800', '100', 'M00367', '1006', null, null, null, null, null, null, 'L04-14-01', 'L04-11-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 16:10:39', 'Quartz', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1617', '0', '0', '0', '800', '100', 'S00022', '1006', null, null, null, null, null, null, 'L04-13-03', 'L04-11-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 16:10:39', 'Quartz', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1618', '0', '0', '10', '700', '100', 'M00015', '1006', null, null, null, null, null, null, 'L01-14-02', 'L01-14-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 16:22:43', 'wjj', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask` VALUES ('1619', '0', '0', '10', '700', '10', 'M00386', '1006', null, null, null, null, null, null, 'L04-08-04', 'L04-08-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 16:33:45', 'my test', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1620', '0', '0', '0', '800', '100', 'M00367', '1006', null, null, null, null, null, null, 'L04-11-05', 'L04-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 16:38:02', 'Quartz', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1621', '0', '0', '0', '800', '10', 'S00022', '1006', null, null, null, null, null, null, 'L04-11-04', 'L04-13-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-25 16:38:10', 'Quartz', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1622', '0', '0', '5', '100', '100', 'M00396', '0', null, null, null, null, null, null, '', 'L04-05-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 11:25:05', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1623', '0', '0', '5', '100', '100', 'M00400', '0', null, null, null, null, null, null, '', 'L04-05-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 11:25:09', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1624', '0', '0', '5', '100', '100', 'M00399', '0', null, null, null, null, null, null, '', 'L04-05-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 11:25:09', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1625', '0', '0', '10', '300', '100', 'M00019', '1001', null, null, null, null, null, null, 'L01-11-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 11:54:58', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1626', '0', '0', '10', '300', '10', 'M00401', '1012', null, null, null, null, null, null, 'L04-06-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 11:54:59', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1627', '0', '0', '10', '300', '100', 'M00020', '1006', null, null, null, null, null, null, 'L01-12-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 12:00:03', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1628', '0', '0', '10', '300', '100', 'M00004', '1001', null, null, null, null, null, null, 'L01-12-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 13:34:28', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1629', '0', '0', '10', '300', '100', 'M00399', '1012', null, null, null, null, null, null, 'L04-05-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 13:36:45', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1630', '0', '0', '5', '100', '100', 'M00399', '0', null, null, null, null, null, null, '', 'L04-06-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 13:41:08', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1631', '0', '0', '10', '700', '10', 'M00384', '1006', null, null, null, null, null, null, 'L04-12-05', 'L04-12-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 13:49:59', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1632', '0', '0', '0', '800', '100', 'M00367', '1006', null, null, null, null, null, null, 'L04-14-01', 'L04-11-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 13:51:03', 'Quartz', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1633', '0', '0', '0', '800', '10', 'M00367', '1006', null, null, null, null, null, null, 'L04-11-05', 'L04-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 14:05:02', 'Quartz', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1634', '0', '0', '10', '300', '100', 'M00123', '1001', null, null, null, null, null, null, 'L01-12-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 14:18:15', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1635', '0', '0', '10', '300', '100', 'M00110', '1012', null, null, null, null, null, null, 'L01-12-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 14:21:26', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1636', '0', '0', '10', '700', '10', 'M00382', '1006', null, null, null, null, null, null, 'L04-07-03', 'L04-07-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-26 16:15:37', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1637', '0', '0', '5', '100', '100', 'M00401', '0', null, null, null, null, null, null, '', 'L04-07-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-27 08:50:44', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1638', '0', '0', '5', '100', '100', 'M00393', '0', null, null, null, null, null, null, '', 'L04-07-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-27 08:50:45', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1639', '0', '0', '10', '300', '100', 'M00401', '1012', null, null, null, null, null, null, 'L04-07-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-27 08:55:00', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1640', '0', '0', '10', '600', '10', 'M00352', '1006', null, null, null, null, null, null, 'L04-12-03', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-27 08:57:13', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1641', '0', '0', '10', '600', '10', 'M00339', '1006', null, null, null, null, null, null, 'L04-12-02', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-27 08:59:40', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1642', '0', '0', '10', '300', '10', 'M00015', '1012', null, null, null, null, null, null, 'L01-14-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-27 09:10:07', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1643', '0', '0', '10', '300', '100', 'M00016', '1012', null, null, null, null, null, null, 'L01-14-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-27 11:08:57', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1644', '0', '0', '10', '600', '100', 'M00375', '1006', null, null, null, null, null, null, 'L04-08-05', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-27 11:33:54', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1645', '0', '0', '5', '100', '100', 'M00401', '0', null, null, null, null, null, null, '', 'L01-01-35-35', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-29 17:14:17', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1646', '0', '0', '10', '300', '100', 'M00031', '1006', null, null, null, null, null, null, 'L04-11-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-29 17:17:07', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1647', '0', '0', '10', '700', '10', 'M00399', '1006', null, null, null, null, null, null, 'L04-06-03', 'L04-06-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-29 17:26:37', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1648', '0', '0', '10', '700', '10', 'M00071', '1006', null, null, null, null, null, null, 'L04-06-01', 'L04-06-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2018-12-29 17:27:08', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask` VALUES ('1649', '0', '0', '5', '100', '100', 'M00335', '0', null, null, null, null, null, null, '', 'L01-03-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-02 08:52:38', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1650', '0', '0', '5', '100', '100', 'M00336', '0', null, null, null, null, null, null, '', 'L01-01-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-02 08:52:39', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1651', '0', '0', '10', '300', '100', 'S00019', '1006', null, null, null, null, null, null, 'L04-09-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-02 09:00:15', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1652', '0', '0', '10', '700', '100', 'M00336', '1006', null, null, null, null, null, null, 'L01-01-03', 'L01-01-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-02 09:52:15', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1653', '0', '0', '10', '700', '100', 'M00335', '1006', null, null, null, null, null, null, 'L01-03-04', 'L01-03-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-02 09:52:17', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1654', '0', '0', '10', '600', '100', 'M00397', '1006', null, null, null, null, null, null, 'L04-07-04', null, null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-02 09:57:37', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1655', '0', '0', '5', '100', '100', 'M00376', '0', null, null, null, null, null, null, '', 'L01-01-76-76', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 16:22:26', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1656', '0', '0', '5', '100', '100', 'S00019', '0', null, null, null, null, null, null, '', 'L01-01-74-74', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 16:22:27', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1657', '0', '0', '10', '300', '100', 'M00376', '1006', null, null, null, null, null, null, 'L01-01-76-76', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 16:25:03', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask` VALUES ('1658', '0', '0', '10', '700', '1', 'M00396', '1006', null, null, null, null, null, null, 'L04-05-02', 'L04-05-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 16:27:55', 'wjj', '2019-08-15 17:20:08', null, '\0');
INSERT INTO `wcstask` VALUES ('1659', '0', '0', '10', '100', '100', 'M00004', '1006', null, null, null, null, null, null, '', 'L01-14-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 17:17:50', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1660', '0', '0', '10', '100', '100', 'M00005', '1006', null, null, null, null, null, null, '', 'L01-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 17:18:15', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1661', '0', '0', '10', '100', '100', 'M00006', '1006', null, null, null, null, null, null, '', 'L01-12-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 17:18:29', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1662', '0', '0', '10', '100', '100', 'M00007', '1006', null, null, null, null, null, null, '', 'L01-12-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 17:18:48', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1663', '0', '0', '10', '100', '100', 'M00008', '1006', null, null, null, null, null, null, '', 'L01-12-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 17:18:59', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1664', '0', '0', '10', '100', '100', 'M00009', '1006', null, null, null, null, null, null, '', 'L04-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 17:19:09', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1665', '0', '0', '10', '100', '100', 'M00010', '1006', null, null, null, null, null, null, '', 'L01-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 17:19:19', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1666', '0', '0', '10', '100', '100', 'M00011', '1006', null, null, null, null, null, null, '', 'L01-11-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-03 17:19:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1667', '0', '0', '10', '100', '100', 'M00012', '1006', null, null, null, null, null, null, '', 'L02-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 09:10:25', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1668', '0', '0', '10', '100', '100', 'M00013', '1006', null, null, null, null, null, null, '', 'L02-13-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 14:02:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1669', '0', '0', '10', '100', '100', 'M00014', '1006', null, null, null, null, null, null, '', 'L02-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 14:56:27', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1670', '0', '0', '10', '100', '100', 'M00016', '1006', null, null, null, null, null, null, '', 'L02-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 14:57:10', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1671', '0', '0', '10', '300', '100', 'M00012', '1006', null, null, null, null, null, null, 'L02-14-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:18:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1672', '0', '0', '10', '300', '100', 'M00013', '1006', null, null, null, null, null, null, 'L02-13-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:19:41', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1673', '0', '0', '10', '300', '100', 'M00014', '1006', null, null, null, null, null, null, 'L02-12-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:20:39', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1674', '0', '0', '10', '300', '100', 'M00016', '1006', null, null, null, null, null, null, 'L02-11-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:20:57', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1675', '0', '0', '10', '300', '100', 'M00004', '1006', null, null, null, null, null, null, 'L01-14-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:21:10', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1676', '0', '0', '10', '300', '100', 'M00005', '1006', null, null, null, null, null, null, 'L01-12-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:21:21', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1677', '0', '0', '10', '300', '100', 'M00006', '1006', null, null, null, null, null, null, 'L01-12-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:21:47', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1678', '0', '0', '10', '300', '100', 'M00007', '1006', null, null, null, null, null, null, 'L01-12-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:22:10', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1679', '0', '0', '10', '300', '100', 'M00008', '1006', null, null, null, null, null, null, 'L01-12-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:22:32', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1680', '0', '0', '10', '300', '100', 'M00009', '1006', null, null, null, null, null, null, 'L04-11-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:22:47', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1681', '0', '0', '10', '300', '100', 'M00010', '1006', null, null, null, null, null, null, 'L01-11-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:23:00', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1682', '0', '0', '10', '300', '100', 'M00011', '1006', null, null, null, null, null, null, 'L01-11-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:23:24', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1683', '0', '0', '10', '100', '100', 'M00020', '1006', null, null, null, null, null, null, '', 'L01-14-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:36:05', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1684', '0', '0', '10', '100', '100', 'M00021', '1006', null, null, null, null, null, null, '', 'L01-12-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:36:30', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1685', '0', '0', '10', '300', '100', 'M00020', '1006', null, null, null, null, null, null, 'L01-14-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:37:17', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1686', '0', '0', '10', '100', '100', 'M00022', '1006', null, null, null, null, null, null, '', 'L01-14-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:38:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1687', '0', '0', '10', '100', '100', 'M00023', '1006', null, null, null, null, null, null, '', 'L01-12-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:39:00', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1688', '0', '0', '10', '100', '100', 'M00025', '1006', null, null, null, null, null, null, '', 'L01-12-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:39:11', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1689', '0', '0', '10', '100', '100', 'M00026', '1006', null, null, null, null, null, null, '', 'L01-12-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:39:28', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1690', '0', '0', '10', '100', '100', 'M00027', '1006', null, null, null, null, null, null, '', 'L04-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:39:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1691', '0', '0', '10', '100', '100', 'M00028', '1006', null, null, null, null, null, null, '', 'L01-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:40:01', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1692', '0', '0', '10', '100', '100', 'M00029', '1006', null, null, null, null, null, null, '', 'L01-11-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:40:13', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1693', '0', '0', '10', '100', '100', 'M00031', '1006', null, null, null, null, null, null, '', 'L01-11-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:40:30', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1694', '0', '0', '10', '100', '100', 'M00032', '1006', null, null, null, null, null, null, '', 'L01-11-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:40:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1695', '0', '0', '10', '100', '100', 'M00034', '1006', null, null, null, null, null, null, '', 'L01-11-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:41:00', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1696', '0', '0', '10', '100', '100', 'M00037', '1006', null, null, null, null, null, null, '', 'L04-10-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:41:28', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1697', '0', '0', '10', '100', '100', 'M00038', '1006', null, null, null, null, null, null, '', 'L01-10-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:41:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1698', '0', '0', '10', '100', '100', 'M00039', '1006', null, null, null, null, null, null, '', 'L01-10-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:41:50', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1699', '0', '0', '10', '100', '100', 'M00040', '1006', null, null, null, null, null, null, '', 'L01-10-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:02', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1700', '0', '0', '10', '100', '100', 'M00041', '1006', null, null, null, null, null, null, '', 'L01-10-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:13', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1701', '0', '0', '10', '100', '100', 'M00042', '1006', null, null, null, null, null, null, '', 'L04-09-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:22', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1702', '0', '0', '10', '100', '100', 'M00043', '1006', null, null, null, null, null, null, '', 'L04-09-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:34', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1703', '0', '0', '10', '100', '100', 'M00044', '1006', null, null, null, null, null, null, '', 'L01-09-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1704', '0', '0', '10', '100', '100', 'M00045', '1006', null, null, null, null, null, null, '', 'L01-09-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:48:22', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1705', '0', '0', '10', '100', '100', 'M00047', '1006', null, null, null, null, null, null, '', 'L01-09-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:48:33', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1706', '0', '0', '10', '100', '100', 'M00048', '1006', null, null, null, null, null, null, '', 'L01-09-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:48:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1707', '0', '0', '10', '100', '100', 'M00049', '1006', null, null, null, null, null, null, '', 'L01-09-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 15:48:52', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1708', '0', '0', '10', '100', '100', 'M00050', '1006', null, null, null, null, null, null, '', 'L02-14-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 16:40:57', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1709', '0', '0', '10', '100', '100', 'M00051', '1006', null, null, null, null, null, null, '', 'L02-09-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 16:49:35', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1710', '0', '0', '10', '100', '100', 'M00052', '1006', null, null, null, null, null, null, '', 'L02-09-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 16:49:58', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1711', '0', '0', '10', '300', '100', 'M00025', '1006', null, null, null, null, null, null, 'L01-12-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 17:12:28', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1712', '0', '0', '10', '300', '100', 'M00026', '1006', null, null, null, null, null, null, 'L01-12-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 17:13:18', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1713', '0', '0', '10', '300', '100', 'M00027', '1006', null, null, null, null, null, null, 'L04-11-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 17:20:04', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1714', '0', '0', '10', '100', '100', 'M00061', '1006', null, null, null, null, null, null, '', 'L01-12-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 17:21:15', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1715', '0', '0', '10', '100', '100', 'M00062', '1006', null, null, null, null, null, null, '', 'L01-12-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 17:21:25', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1716', '0', '0', '10', '100', '100', 'M00063', '1006', null, null, null, null, null, null, '', 'L04-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 17:21:42', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1717', '0', '0', '10', '300', '100', 'M00022', '1006', null, null, null, null, null, null, 'L01-14-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:11:51', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1718', '0', '0', '15', '800', '100', 'M00050', '1006', null, null, null, null, null, null, 'L02-14-05', 'L02-14-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:11:51', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1719', '0', '0', '10', '300', '100', 'M00028', '1006', null, null, null, null, null, null, 'L01-11-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:12:15', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1720', '0', '0', '10', '300', '100', 'M00029', '1006', null, null, null, null, null, null, 'L01-11-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:13:18', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1721', '0', '0', '10', '300', '100', 'M00031', '1006', null, null, null, null, null, null, 'L01-11-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:13:47', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1722', '0', '0', '10', '100', '100', 'M00072', '1006', null, null, null, null, null, null, '', 'L01-14-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:14:30', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1723', '0', '0', '10', '100', '100', 'M00073', '1006', null, null, null, null, null, null, '', 'L01-11-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:14:39', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1724', '0', '0', '10', '100', '100', 'M00074', '1006', null, null, null, null, null, null, '', 'L01-11-02', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:14:48', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1725', '0', '0', '10', '100', '100', 'M00075', '1006', null, null, null, null, null, null, '', 'L01-11-03', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:14:59', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1726', '0', '0', '10', '100', '100', 'M00076', '1006', null, null, null, null, null, null, '', 'L04-08-01', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:15:08', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1727', '0', '0', '10', '300', '100', 'M00032', '1006', null, null, null, null, null, null, 'L01-11-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:24:52', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1728', '0', '0', '10', '300', '100', 'M00034', '1006', null, null, null, null, null, null, 'L01-11-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:49:49', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1729', '0', '0', '10', '300', '100', 'M00037', '1006', null, null, null, null, null, null, 'L04-10-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:53:41', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1730', '0', '0', '10', '300', '100', 'M00038', '1006', null, null, null, null, null, null, 'L01-10-02', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:55:27', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1731', '0', '0', '10', '100', '100', 'M00081', '1006', null, null, null, null, null, null, '', 'L01-11-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:58:17', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1732', '0', '0', '10', '100', '100', 'M00082', '1006', null, null, null, null, null, null, '', 'L01-11-05', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:58:26', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1733', '0', '0', '10', '100', '100', 'M00083', '1006', null, null, null, null, null, null, '', 'L04-10-04', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 18:58:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1734', '0', '0', '10', '300', '10', 'M00039', '1006', null, null, null, null, null, null, 'L01-10-03', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 19:00:46', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1735', '0', '0', '10', '300', '10', 'M00040', '1006', null, null, null, null, null, null, 'L01-10-04', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 19:07:34', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1736', '0', '0', '10', '300', '10', 'M00041', '1006', null, null, null, null, null, null, 'L01-10-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 19:09:48', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1737', '0', '0', '10', '300', '10', 'M00042', '1006', null, null, null, null, null, null, 'L04-09-01', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 19:12:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1738', '0', '0', '10', '300', '10', 'M00043', '1006', null, null, null, null, null, null, 'L04-09-05', '', null, '1', '0', '0', '0', null, '0', 'XT0001', '2019-01-04 19:12:44', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask` VALUES ('1739', '0', '80', '100', '100', '1', 'M00008', '0', '1000', '1', null, null, null, null, '0', 'L01-01-01-01', 'wms', '2', '0', '0', '0', null, '0', 'A0001', '2019-11-04 15:50:23', 'WCS_Interface', '2019-11-04 15:50:23', null, '\0');

-- ----------------------------
-- Table structure for wcstaskdetail
-- ----------------------------
DROP TABLE IF EXISTS `wcstaskdetail`;
CREATE TABLE `wcstaskdetail` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '立库任务明细ID',
  `taskId` int(11) NOT NULL COMMENT '主任务Id',
  `referLineId` int(11) NOT NULL,
  `materialCode` varchar(50) DEFAULT '' COMMENT '物料编码',
  `materialName` varchar(500) DEFAULT '',
  `billCode` varchar(50) DEFAULT NULL COMMENT '订单号',
  `qty` decimal(12,3) DEFAULT NULL COMMENT '数量',
  `weight` double(16,0) DEFAULT '0' COMMENT '重量',
  `unit` varchar(20) DEFAULT NULL COMMENT '单位',
  `lastUpdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最后修改时间',
  `lastUpdatedBy` varchar(50) DEFAULT NULL COMMENT '更新用户',
  `deleted` bit(1) NOT NULL DEFAULT b'0' COMMENT '删除标记',
  PRIMARY KEY (`id`) USING BTREE,
  KEY `id` (`taskId`,`referLineId`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1375 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='立库任务明细表';

-- ----------------------------
-- Records of wcstaskdetail
-- ----------------------------
INSERT INTO `wcstaskdetail` VALUES ('1148', '1387', '0', '9800000000000006', '管、缆、线部件借用件', 'M00382', '100.000', '300', null, '2018-12-11 18:44:00', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1149', '1388', '0', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00405', '200.000', '400', null, '2018-12-11 18:43:58', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1150', '1389', '0', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00404', '300.000', '500', null, '2018-12-11 18:43:55', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1151', '1390', '0', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-11 18:47:25', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1153', '1394', '0', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00404', '300.000', '500', null, '2018-12-11 19:20:04', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1154', '1395', '0', '9800000000000006', '管、缆、线部件借用件', 'M00382', '100.000', '300', null, '2018-12-11 19:20:06', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1155', '1396', '0', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-11 19:28:30', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1156', '1397', '0', '9800000000000006', '管、缆、线部件借用件', 'M00399', '50.000', '100', null, '2018-12-11 19:28:27', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1157', '1398', '0', '9800000000000006', '管、缆、线部件借用件', 'M00398', '100.000', '200', null, '2018-12-11 19:28:25', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1158', '1399', '0', '9800000000000006', '管、缆、线部件借用件', 'M00397', '200.000', '400', null, '2018-12-11 19:28:21', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1159', '1400', '0', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00395', '100.000', '200', null, '2018-12-11 19:28:23', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1160', '1401', '0', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00394', '200.000', '400', null, '2018-12-11 19:28:18', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1161', '1402', '0', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00393', '300.000', '600', null, '2018-12-11 19:28:16', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1162', '1403', '0', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00392', '100.000', '200', null, '2018-12-11 19:28:14', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1163', '1404', '0', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00390', '300.000', '600', null, '2018-12-11 19:28:13', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1164', '1405', '0', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00391', '200.000', '400', null, '2018-12-11 19:28:11', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1166', '1409', '0', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00390', '300.000', '600', null, '2018-12-12 08:48:58', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1167', '1410', '0', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00405', '200.000', '400', null, '2018-12-12 08:48:59', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1168', '1411', '0', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00395', '100.000', '200', null, '2018-12-12 08:49:00', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1169', '1412', '0', '9800000000000006', '管、缆、线部件借用件', 'M00399', '50.000', '100', null, '2018-12-12 08:49:00', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1170', '1413', '0', '9800000000000006', '管、缆、线部件借用件', 'M00398', '100.000', '200', null, '2018-12-12 08:49:03', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1171', '1414', '0', '5000000000000028', 'LNG分子筛包环缝专机', 'M00370', '220.000', '380', null, '2018-12-12 09:35:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1172', '1415', '0', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00368', '300.000', '320', null, '2018-12-12 09:35:15', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1173', '1416', '0', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 09:39:43', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1174', '1417', '0', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00405', '20.000', '88', null, '2018-12-12 09:44:44', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1175', '1418', '0', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00404', '20.000', '23', null, '2018-12-12 09:44:46', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1176', '1419', '0', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00403', '20.000', '88', null, '2018-12-12 09:44:49', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1177', '1420', '0', '6301003400000000', '等离子火焰切割机', 'M00003', '10.000', '26', null, '2018-12-12 09:45:28', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1178', '1420', '0', '6301003400000000', '等离子火焰切割机', 'M00003', '5.000', '13', null, '2018-12-12 09:45:28', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1179', '1421', '0', '6301003400000000', '等离子火焰切割机', 'M00367', '20.000', '50', null, '2018-12-12 09:45:45', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1180', '1422', '0', '6301003400000000', '等离子火焰切割机', 'M00369', '200.000', '130', null, '2018-12-12 09:45:36', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1181', '1423', '0', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00393', '300.000', '600', null, '2018-12-12 09:46:46', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1182', '1424', '0', '9800000000000006', '管、缆、线部件借用件', 'M00397', '200.000', '400', null, '2018-12-12 09:46:47', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1183', '1430', '0', '5000000000000028', 'LNG分子筛包环缝专机', 'M00370', '50.000', '380', null, '2018-12-12 10:30:02', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1184', '1438', '0', '5000000000000028', 'LNG分子筛包环缝专机', 'M00391', '200.000', '320', null, '2018-12-12 13:36:26', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1185', '1439', '0', '9800000000000008', '旋转通用部件', 'M00402', '300.000', '380', null, '2018-12-12 15:11:48', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1186', '1440', '0', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 13:16:03', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1187', '1441', '0', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 13:37:11', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1188', '1442', '0', '0001010203010000', '45方钢', 'M00005', '100.000', '0', null, '2018-12-12 13:38:58', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1189', '1443', '0', '0001010203010000', '45方钢', 'M00005', '100.000', '0', null, '2018-12-12 13:39:40', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1190', '1444', '0', '0001010203010000', '45方钢', 'M00010', '1000.000', '0', null, '2018-12-12 13:47:53', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1191', '1445', '0', '0001010203010000', '45方钢', 'M00010', '1000.000', '0', null, '2018-12-12 13:48:13', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1192', '1446', '0', '6301003400000000', '等离子火焰切割机', 'M00369', '200.000', '130', null, '2018-12-12 14:05:37', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1193', '1447', '0', '6301003400000000', '等离子火焰切割机', 'M00367', '20.000', '50', null, '2018-12-12 14:05:38', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1194', '1449', '0', '0001010101004000', 'A3元钢', 'M00001', '100.000', '0', null, '2018-12-12 14:28:46', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1195', '1449', '0', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 14:28:46', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1196', '1450', '0', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 14:30:06', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1197', '1452', '0', '5000000000000028', 'LNG分子筛包环缝专机', 'M00378', '50.000', '65', null, '2018-12-12 14:43:15', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1198', '1453', '0', '5000000000000028', 'LNG分子筛包环缝专机', 'L00320', '50.000', '65', null, '2018-12-12 14:43:08', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1199', '1454', '0', '5000000000000028', 'LNG分子筛包环缝专机', 'S00021', '100.000', '130', null, '2018-12-12 14:42:53', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1200', '1455', '0', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 14:43:41', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1201', '1458', '0', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 14:46:50', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1202', '1459', '0', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 14:48:24', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1209', '1466', '0', '5000000000000028', 'LNG分子筛包环缝专机', 'M00378', '50.000', '65', null, '2018-12-12 15:12:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1210', '1467', '0', '5000000000000028', 'LNG分子筛包环缝专机', 'L00320', '50.000', '65', null, '2018-12-12 15:12:02', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1211', '1468', '0', '5000000000000028', 'LNG分子筛包环缝专机', 'S00021', '100.000', '130', null, '2018-12-12 15:12:03', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1212', '1470', '0', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00367', '100.000', '883', null, '2018-12-12 15:19:16', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1213', '1471', '0', '2102041320113700', '散热器', 'M00374', '100.000', '2234', null, '2018-12-12 15:22:58', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1214', '1472', '0', '2102041320113700', '散热器', 'M00373', '100.000', '676', null, '2018-12-12 15:23:00', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1215', '1473', '0', '2102041320113700', '散热器', 'M00372', '100.000', '6575', null, '2018-12-12 15:23:02', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1216', '1474', '0', '2102041320113700', '散热器', 'M00369', '100.000', '655', null, '2018-12-12 15:23:05', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1217', '1476', '0', '0001010203010000', '45方钢', 'M00004', '100.000', '0', null, '2018-12-12 15:24:31', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1218', '1477', '0', '0001010203010000', '45方钢', 'M00004', '100.000', '0', null, '2018-12-12 15:24:56', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1219', '1479', '0', '2102041320113700', '散热器', 'M00369', '100.000', '655', null, '2018-12-12 15:29:20', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1220', '1480', '0', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 18:47:14', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1221', '1481', '0', '0001010102006000', '45元钢', 'M00002', '100.000', '0', null, '2018-12-12 18:57:56', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1223', '1486', '0', '9800000000000006', '管、缆、线部件借用件', 'M00377', '100.000', '200', null, '2018-12-12 19:18:02', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1224', '1487', '0', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00404', '20.000', '23', null, '2018-12-12 19:20:35', 'ricard', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1225', '1488', '0', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 19:25:10', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1226', '1490', '0', '0001010203010000', '45方钢', 'M00002', '100.000', '0', null, '2018-12-12 19:25:15', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1227', '1491', '0', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 19:41:55', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1228', '1493', '0', '6301003400000000', '等离子火焰切割机', 'S00018', '100.000', '130', null, '2018-12-13 08:49:23', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1229', '1494', '0', '6000000000000680', '油缸自动焊接系统', 'L00320', '200.000', '130', null, '2018-12-13 08:49:39', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1230', '1495', '0', '6000000000000680', '油缸自动焊接系统', 'M00379', '50.000', '100', null, '2018-12-13 09:25:45', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1231', '1496', '0', '6000000000000680', '油缸自动焊接系统', 'M00378', '100.000', '200', null, '2018-12-13 09:25:55', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1232', '1497', '0', '6000000000000680', '油缸自动焊接系统', 'M00376', '60.000', '120', null, '2018-12-13 09:26:00', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1233', '1498', '0', '6000000000000143', '低温气瓶外胆环缝TIG', 'S00021', '300.000', '6000', null, '2018-12-13 09:26:06', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1234', '1499', '0', '6000000000000680', '油缸自动焊接系统', 'M00380', '200.000', '600', null, '2018-12-13 09:29:40', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1235', '1500', '0', '6000000000000680', '油缸自动焊接系统', 'M00376', '60.000', '120', null, '2018-12-13 09:36:07', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1236', '1501', '0', '6000000000000680', '油缸自动焊接系统', 'M00378', '100.000', '200', null, '2018-12-13 09:36:08', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1237', '1502', '0', '6000000000000680', '油缸自动焊接系统', 'M00379', '50.000', '100', null, '2018-12-13 09:36:09', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1238', '1503', '0', '6000000000000680', '油缸自动焊接系统', 'M00380', '200.000', '600', null, '2018-12-13 09:36:10', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1239', '1504', '0', '2102041320113700', '散热器', 'M00373', '100.000', '676', null, '2018-12-13 09:45:28', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1240', '1514', '0', '6301003400000000', '等离子火焰切割机', 'S00015', '200.000', '400', null, '2018-12-14 08:57:29', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1241', '1515', '0', '9800000000000008', '旋转通用部件', 'S00019', '200.000', '420', null, '2018-12-14 08:57:23', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1242', '1517', '0', '0001010203010000', '45方钢', 'M00015', '100.000', '0', null, '2018-12-17 17:05:54', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1243', '1518', '0', '0001010203010000', '45方钢', 'M00016', '1.000', '0', null, '2018-12-25 15:55:40', 'tony', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1244', '1519', '0', '0001010203010000', '45方钢', 'M00018', '100.000', '0', null, '2018-12-25 15:56:26', 'tony', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1245', '1520', '0', '0001010203010000', '45方钢', 'M00020', '100.000', '0', null, '2018-12-25 15:55:54', 'tony', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1246', '1521', '0', '0001010203010000', '45方钢', 'M00030', '100.000', '0', null, '2018-12-25 15:56:22', 'tony', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1247', '1522', '0', '0001010203010000', '45方钢', 'M00031', '50.000', '0', null, '2018-12-25 15:56:19', 'tony', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1248', '1523', '0', '0001010203010000', '45方钢', 'M00110', '100.000', '0', null, '2018-12-25 15:56:15', 'tony', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1249', '1524', '0', '0001010203010000', '45方钢', 'M00111', '100.000', '0', null, '2018-12-25 15:56:12', 'tony', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1250', '1525', '0', '0001010203010000', '45方钢', 'M00123', '100.000', '0', null, '2018-12-25 15:56:08', 'tony', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1251', '1526', '0', '0001010203010000', '45方钢', 'M00060', '100.000', '50', null, '2018-12-17 19:25:40', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1252', '1527', '0', '1000', '测试物料', 'M00200', '100.000', '100', null, '2018-12-17 19:50:04', 'abc', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1253', '1528', '0', '0001010203010000', '45方钢', 'M00019', '100.000', '50', null, '2018-12-18 08:55:55', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1254', '1529', '0', '0001010203010000', '45方钢', 'M00004', '100.000', '0', null, '2018-12-25 15:56:05', 'tony', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1255', '1530', '0', '9800000000000015', 'TC36总装图借用部件', 'M00382', '200.000', '0', null, '2018-12-18 11:50:16', 'my test', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1256', '1531', '0', '2102041320113700', '散热器', 'M00385', '300.000', '250', null, '2018-12-18 11:49:58', 'my test', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1257', '1533', '0', '9800000000000015', 'TC36总装图借用部件', 'M00382', '200.000', '0', null, '2018-12-18 16:34:16', 'my test', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1258', '1534', '0', '2102041320113700', '散热器', 'M00385', '300.000', '250', null, '2018-12-18 16:34:19', 'my test', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1259', '1548', '0', '2102041320113700', '散热器', 'M00356', '100.000', '88', null, '2018-12-24 08:48:12', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1260', '1549', '0', '2102041320113700', '散热器', 'M00355', '100.000', '98', null, '2018-12-24 08:48:15', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1261', '1550', '0', '2102041320113700', '散热器', 'M00354', '100.000', '76', null, '2018-12-24 08:48:17', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1262', '1551', '0', '2102041320113700', '散热器', 'M00353', '100.000', '76', null, '2018-12-24 08:48:20', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1263', '1552', '0', '2102041320113700', '散热器', 'M00352', '100.000', '67', null, '2018-12-24 08:48:22', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1264', '1553', '0', '2102041320113700', '散热器', 'M00351', '100.000', '88', null, '2018-12-24 08:48:25', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1265', '1554', '0', '9800000000000015', 'TC36总装图借用部件', 'M00384', '200.000', '0', null, '2018-12-24 08:48:27', 'xqs', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1266', '1609', '0', '9800000000000006', '管、缆、线部件借用件', 'M00401', '100.000', '210', null, '2018-12-25 15:16:10', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1267', '1610', '0', '9800000000000007', 'TP040管板焊头外构件', 'M00400', '200.000', '260', null, '2018-12-25 15:16:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1268', '1611', '0', '9800000000000007', 'TP040管板焊头外构件', 'M00400', '200.000', '260', null, '2018-12-25 15:22:06', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1269', '1612', '0', '9800000000000006', '管、缆、线部件借用件', 'M00377', '100.000', '200', null, '2018-12-25 15:22:07', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1270', '1613', '0', '2102041320113700', '散热器', 'M00372', '100.000', '6575', null, '2018-12-25 15:27:32', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1271', '1622', '0', '9800000000000006', '管、缆、线部件借用件', 'M00396', '100.000', '156', null, '2018-12-26 11:51:05', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1272', '1623', '0', '0001010203010000', '45方钢', 'M00400', '120.000', '150', null, '2018-12-26 11:50:59', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1273', '1624', '0', '6000000000000195', '低温气瓶内胆环缝焊接系统', 'M00399', '200.000', '210', null, '2018-12-26 11:50:52', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1274', '1625', '0', '0001010203010000', '45方钢', 'M00019', '100.000', '50', null, '2018-12-26 11:56:02', 'Quartz', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1275', '1626', '0', '9800000000000006', '管、缆、线部件借用件', 'M00401', '100.000', '210', null, '2018-12-26 11:56:05', 'Quartz', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1276', '1627', '0', '0001010203010000', '45方钢', 'M00020', '100.000', '0', null, '2018-12-26 12:02:02', 'Quartz', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1277', '1628', '0', '0001010203010000', '45方钢', 'M00004', '100.000', '0', null, '2018-12-26 13:34:23', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1278', '1629', '0', '6000000000000195', '低温气瓶内胆环缝焊接系统', 'M00399', '200.000', '210', null, '2018-12-26 13:36:40', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1279', '1630', '0', '2102041320113700', '散热器', 'M00399', '300.000', '260', null, '2018-12-26 13:41:17', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1280', '1634', '0', '0001010203010000', '45方钢', 'M00123', '100.000', '0', null, '2018-12-26 14:22:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1281', '1635', '0', '0001010203010000', '45方钢', 'M00110', '100.000', '0', null, '2018-12-26 14:21:20', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1282', '1637', '0', '6000000000000195', '低温气瓶内胆环缝焊接系统', 'M00401', '100.000', '150', null, '2018-12-27 08:50:53', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1283', '1638', '0', '0001010203010000', '45方钢', 'M00393', '200.000', '0', null, '2018-12-27 08:51:00', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1284', '1639', '0', '6000000000000195', '低温气瓶内胆环缝焊接系统', 'M00401', '100.000', '150', null, '2018-12-27 08:54:55', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1285', '1642', '0', '0001010203010000', '45方钢', 'M00015', '80.000', '0', null, '2018-12-27 09:12:02', 'Quartz', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1286', '1643', '0', '0001010203010000', '45方钢', 'M00016', '1.000', '0', null, '2018-12-27 11:08:51', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1287', '1645', '0', '9800000000000000', '必备附件借用件', 'M00401', '200.000', '500', null, '2018-12-29 17:14:22', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1288', '1646', '0', '0001010203010000', '45方钢', 'M00031', '50.000', '0', null, '2018-12-29 17:17:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1289', '1649', '0', '5200000000000033', '2-8”定长切割坡口一体机', 'M00335', '200.000', '160', null, '2019-01-02 08:53:12', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1290', '1650', '0', '3600001702001600', '导轨支撑腿', 'M00336', '200.000', '200', null, '2019-01-02 08:53:07', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1291', '1651', '0', '9800000000000008', '旋转通用部件', 'S00019', '200.000', '420', null, '2019-01-02 09:00:15', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1292', '1655', '0', '5200000000000077', '426端面坡口机系统', 'M00376', '100.000', '630', null, '2019-01-03 16:22:49', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1293', '1656', '0', '6000000000000150', '边梁P+T自动焊接系统', 'S00019', '200.000', '62', null, '2019-01-03 16:22:42', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1294', '1657', '0', '5200000000000077', '426端面坡口机系统', 'M00376', '100.000', '630', null, '2019-01-03 16:25:05', 'wjj', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1295', '1659', '0', '123456', '测试', 'M00004', '100.000', '0', null, '2019-01-04 09:02:08', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1296', '1660', '0', '123456', '测试', 'M00005', '100.000', '0', null, '2019-01-04 09:02:28', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1297', '1661', '0', '123456', '测试', 'M00006', '100.000', '0', null, '2019-01-04 09:02:45', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1298', '1662', '0', '123456', '测试', 'M00007', '100.000', '0', null, '2019-01-04 09:09:10', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1299', '1663', '0', '123456', '测试', 'M00008', '100.000', '0', null, '2019-01-04 09:09:13', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1300', '1664', '0', '123456', '测试', 'M00009', '100.000', '0', null, '2019-01-04 09:09:16', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1301', '1665', '0', '123456', '测试', 'M00010', '100.000', '0', null, '2019-01-04 09:09:22', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1302', '1666', '0', '123456', '测试', 'M00011', '100.000', '0', null, '2019-01-04 09:09:29', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1303', '1667', '0', '123456', '测试', 'M00012', '100.000', '0', null, '2019-01-04 09:10:33', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1304', '1668', '0', '123456', '测试', 'M00013', '100.000', '0', null, '2019-01-04 14:03:12', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1305', '1669', '0', '123456', '测试', 'M00014', '100.000', '0', null, '2019-01-04 14:57:33', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1306', '1670', '0', '123456', '测试', 'M00016', '100.000', '0', null, '2019-01-04 14:57:39', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1307', '1671', '0', '123456', '测试', 'M00012', '100.000', '0', null, '2019-01-04 15:18:56', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1308', '1672', '0', '123456', '测试', 'M00013', '100.000', '0', null, '2019-01-04 15:19:41', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1309', '1673', '0', '123456', '测试', 'M00014', '100.000', '0', null, '2019-01-04 15:20:40', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1310', '1674', '0', '123456', '测试', 'M00016', '100.000', '0', null, '2019-01-04 15:20:57', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1311', '1675', '0', '123456', '测试', 'M00004', '100.000', '0', null, '2019-01-04 15:21:11', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1312', '1676', '0', '123456', '测试', 'M00005', '100.000', '0', null, '2019-01-04 15:21:22', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1313', '1677', '0', '123456', '测试', 'M00006', '100.000', '0', null, '2019-01-04 15:21:48', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1314', '1678', '0', '123456', '测试', 'M00007', '100.000', '0', null, '2019-01-04 15:22:11', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1315', '1679', '0', '123456', '测试', 'M00008', '100.000', '0', null, '2019-01-04 15:22:33', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1316', '1680', '0', '123456', '测试', 'M00009', '100.000', '0', null, '2019-01-04 15:22:47', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1317', '1681', '0', '123456', '测试', 'M00010', '100.000', '0', null, '2019-01-04 15:23:01', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1318', '1682', '0', '123456', '测试', 'M00011', '100.000', '0', null, '2019-01-04 15:23:25', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1319', '1683', '0', '123456', '测试', 'M00020', '100.000', '0', null, '2019-01-04 15:36:49', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1320', '1684', '0', '123456', '测试', 'M00021', '100.000', '0', null, '2019-01-04 15:36:53', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1321', '1685', '0', '123456', '测试', 'M00020', '100.000', '0', null, '2019-01-04 15:37:17', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1322', '1686', '0', '123456', '测试', 'M00022', '100.000', '0', null, '2019-01-04 15:49:47', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1323', '1687', '0', '123456', '测试', 'M00023', '100.000', '0', null, '2019-01-04 15:49:51', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1324', '1688', '0', '123456', '测试', 'M00025', '100.000', '0', null, '2019-01-04 15:49:55', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1325', '1689', '0', '123456', '测试', 'M00026', '100.000', '0', null, '2019-01-04 15:49:59', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1326', '1690', '0', '123456', '测试', 'M00027', '100.000', '0', null, '2019-01-04 15:50:03', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1327', '1691', '0', '123456', '测试', 'M00028', '100.000', '0', null, '2019-01-04 15:50:06', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1328', '1692', '0', '123456', '测试', 'M00029', '100.000', '0', null, '2019-01-04 15:50:10', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1329', '1693', '0', '123456', '测试', 'M00031', '100.000', '0', null, '2019-01-04 15:50:13', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1330', '1694', '0', '123456', '测试', 'M00032', '100.000', '0', null, '2019-01-04 15:50:18', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1331', '1695', '0', '123456', '测试', 'M00034', '100.000', '0', null, '2019-01-04 15:50:21', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1332', '1696', '0', '123456', '测试', 'M00037', '100.000', '0', null, '2019-01-04 15:50:24', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1333', '1697', '0', '123456', '测试', 'M00038', '100.000', '0', null, '2019-01-04 15:50:27', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1334', '1698', '0', '123456', '测试', 'M00039', '100.000', '0', null, '2019-01-04 15:50:31', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1335', '1699', '0', '123456', '测试', 'M00040', '100.000', '0', null, '2019-01-04 15:50:34', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1336', '1700', '0', '123456', '测试', 'M00041', '100.000', '0', null, '2019-01-04 15:50:38', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1337', '1701', '0', '123456', '测试', 'M00042', '100.000', '0', null, '2019-01-04 15:51:39', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1338', '1702', '0', '123456', '测试', 'M00043', '100.000', '0', null, '2019-01-04 15:51:42', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1339', '1703', '0', '123456', '测试', 'M00044', '100.000', '0', null, '2019-01-04 15:51:46', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1340', '1704', '0', '123456', '测试', 'M00045', '100.000', '0', null, '2019-01-04 15:51:49', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1341', '1705', '0', '123456', '测试', 'M00047', '100.000', '0', null, '2019-01-04 15:51:53', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1342', '1706', '0', '123456', '测试', 'M00048', '100.000', '0', null, '2019-01-04 15:52:05', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1343', '1707', '0', '123456', '测试', 'M00049', '100.000', '0', null, '2019-01-04 15:52:14', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1344', '1708', '0', '123456', '测试', 'M00050', '100.000', '0', null, '2019-01-04 16:43:56', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1345', '1709', '0', '123456', '测试', 'M00051', '100.000', '0', null, '2019-01-04 16:50:40', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1346', '1710', '0', '123456', '测试', 'M00052', '100.000', '0', null, '2019-01-04 16:50:45', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1347', '1711', '0', '123456', '测试', 'M00025', '100.000', '0', null, '2019-01-04 17:12:29', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1348', '1712', '0', '123456', '测试', 'M00026', '100.000', '0', null, '2019-01-04 17:13:19', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1349', '1713', '0', '123456', '测试', 'M00027', '100.000', '0', null, '2019-01-04 17:20:05', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1350', '1714', '0', '123456', '测试', 'M00061', '100.000', '0', null, '2019-01-04 17:22:29', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1351', '1715', '0', '123456', '测试', 'M00062', '100.000', '0', null, '2019-01-04 17:22:35', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1352', '1716', '0', '123456', '测试', 'M00063', '100.000', '0', null, '2019-01-04 17:22:38', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1353', '1717', '0', '123456', '测试', 'M00022', '100.000', '0', null, '2019-01-04 18:11:52', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1354', '1719', '0', '123456', '测试', 'M00028', '100.000', '0', null, '2019-01-04 18:12:16', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1355', '1720', '0', '123456', '测试', 'M00029', '100.000', '0', null, '2019-01-04 18:13:18', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1356', '1721', '0', '123456', '测试', 'M00031', '100.000', '0', null, '2019-01-04 18:13:48', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1357', '1722', '0', '123456', '测试', 'M00072', '100.000', '0', null, '2019-01-04 18:16:26', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1358', '1723', '0', '123456', '测试', 'M00073', '100.000', '0', null, '2019-01-04 18:16:30', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1359', '1724', '0', '123456', '测试', 'M00074', '100.000', '0', null, '2019-01-04 18:16:36', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1360', '1725', '0', '123456', '测试', 'M00075', '100.000', '0', null, '2019-01-04 18:16:39', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1361', '1726', '0', '123456', '测试', 'M00076', '100.000', '0', null, '2019-01-04 18:16:44', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1362', '1727', '0', '123456', '测试', 'M00032', '100.000', '0', null, '2019-01-04 18:24:53', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1363', '1728', '0', '123456', '测试', 'M00034', '100.000', '0', null, '2019-01-04 18:49:49', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1364', '1729', '0', '123456', '测试', 'M00037', '100.000', '0', null, '2019-01-04 18:53:42', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1365', '1730', '0', '123456', '测试', 'M00038', '100.000', '0', null, '2019-01-04 18:55:28', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1366', '1731', '0', '123456', '测试', 'M00081', '100.000', '0', null, '2019-01-04 18:59:39', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1367', '1732', '0', '123456', '测试', 'M00082', '100.000', '0', null, '2019-01-04 18:59:44', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1368', '1733', '0', '123456', '测试', 'M00083', '100.000', '0', null, '2019-01-04 18:59:49', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1369', '1734', '0', '123456', '测试', 'M00039', '100.000', '0', null, '2019-01-04 19:00:47', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1370', '1735', '0', '123456', '测试', 'M00040', '100.000', '0', null, '2019-01-04 19:07:35', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1371', '1736', '0', '123456', '测试', 'M00041', '100.000', '0', null, '2019-01-04 19:09:48', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1372', '1737', '0', '123456', '测试', 'M00042', '100.000', '0', null, '2019-01-04 19:12:08', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1373', '1738', '0', '123456', '测试', 'M00043', '100.000', '0', null, '2019-01-04 19:12:45', 'youjie', '\0');
INSERT INTO `wcstaskdetail` VALUES ('1374', '1739', '95', '13904000103', '黑色扁平电缆（2.5mm）', null, '10.000', '0', 'PCS', '2019-11-04 15:53:19', null, '\0');

-- ----------------------------
-- Table structure for wcstaskdetail_deleted
-- ----------------------------
DROP TABLE IF EXISTS `wcstaskdetail_deleted`;
CREATE TABLE `wcstaskdetail_deleted` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '立库任务明细ID',
  `taskId` int(11) NOT NULL COMMENT '主任务Id',
  `materialCode` varchar(50) DEFAULT '' COMMENT '物料编码',
  `materialName` varchar(500) DEFAULT '',
  `billCode` varchar(50) DEFAULT NULL COMMENT '订单号',
  `qty` decimal(12,3) DEFAULT NULL COMMENT '数量',
  `weight` double(16,0) DEFAULT '0' COMMENT '重量',
  `unit` varchar(20) DEFAULT NULL COMMENT '单位',
  `lastUpdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最后修改时间',
  `lastUpdatedBy` varchar(50) DEFAULT NULL COMMENT '更新用户',
  `deleted` bit(1) NOT NULL DEFAULT b'0' COMMENT '删除标记',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1374 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='立库任务明细表';

-- ----------------------------
-- Records of wcstaskdetail_deleted
-- ----------------------------
INSERT INTO `wcstaskdetail_deleted` VALUES ('1148', '1387', '9800000000000006', '管、缆、线部件借用件', 'M00382', '100.000', '300', null, '2018-12-11 18:44:00', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1149', '1388', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00405', '200.000', '400', null, '2018-12-11 18:43:58', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1150', '1389', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00404', '300.000', '500', null, '2018-12-11 18:43:55', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1151', '1390', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-11 18:47:25', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1153', '1394', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00404', '300.000', '500', null, '2018-12-11 19:20:04', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1154', '1395', '9800000000000006', '管、缆、线部件借用件', 'M00382', '100.000', '300', null, '2018-12-11 19:20:06', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1155', '1396', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-11 19:28:30', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1156', '1397', '9800000000000006', '管、缆、线部件借用件', 'M00399', '50.000', '100', null, '2018-12-11 19:28:27', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1157', '1398', '9800000000000006', '管、缆、线部件借用件', 'M00398', '100.000', '200', null, '2018-12-11 19:28:25', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1158', '1399', '9800000000000006', '管、缆、线部件借用件', 'M00397', '200.000', '400', null, '2018-12-11 19:28:21', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1159', '1400', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00395', '100.000', '200', null, '2018-12-11 19:28:23', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1160', '1401', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00394', '200.000', '400', null, '2018-12-11 19:28:18', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1161', '1402', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00393', '300.000', '600', null, '2018-12-11 19:28:16', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1162', '1403', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00392', '100.000', '200', null, '2018-12-11 19:28:14', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1163', '1404', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00390', '300.000', '600', null, '2018-12-11 19:28:13', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1164', '1405', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00391', '200.000', '400', null, '2018-12-11 19:28:11', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1166', '1409', '5000000000000014', '低温气瓶封头L形变位机焊接系统', 'M00390', '300.000', '600', null, '2018-12-12 08:48:58', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1167', '1410', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00405', '200.000', '400', null, '2018-12-12 08:48:59', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1168', '1411', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00395', '100.000', '200', null, '2018-12-12 08:49:00', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1169', '1412', '9800000000000006', '管、缆、线部件借用件', 'M00399', '50.000', '100', null, '2018-12-12 08:49:00', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1170', '1413', '9800000000000006', '管、缆、线部件借用件', 'M00398', '100.000', '200', null, '2018-12-12 08:49:03', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1171', '1414', '5000000000000028', 'LNG分子筛包环缝专机', 'M00370', '220.000', '380', null, '2018-12-12 09:35:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1172', '1415', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00368', '300.000', '320', null, '2018-12-12 09:35:15', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1173', '1416', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 09:39:43', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1174', '1417', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00405', '20.000', '88', null, '2018-12-12 09:44:44', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1175', '1418', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00404', '20.000', '23', null, '2018-12-12 09:44:46', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1176', '1419', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00403', '20.000', '88', null, '2018-12-12 09:44:49', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1177', '1420', '6301003400000000', '等离子火焰切割机', 'M00003', '10.000', '26', null, '2018-12-12 09:45:28', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1178', '1420', '6301003400000000', '等离子火焰切割机', 'M00003', '5.000', '13', null, '2018-12-12 09:45:28', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1179', '1421', '6301003400000000', '等离子火焰切割机', 'M00367', '20.000', '50', null, '2018-12-12 09:45:45', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1180', '1422', '6301003400000000', '等离子火焰切割机', 'M00369', '200.000', '130', null, '2018-12-12 09:45:36', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1181', '1423', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00393', '300.000', '600', null, '2018-12-12 09:46:46', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1182', '1424', '9800000000000006', '管、缆、线部件借用件', 'M00397', '200.000', '400', null, '2018-12-12 09:46:47', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1183', '1430', '5000000000000028', 'LNG分子筛包环缝专机', 'M00370', '50.000', '380', null, '2018-12-12 10:30:02', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1184', '1438', '5000000000000028', 'LNG分子筛包环缝专机', 'M00391', '200.000', '320', null, '2018-12-12 13:36:26', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1185', '1439', '9800000000000008', '旋转通用部件', 'M00402', '300.000', '380', null, '2018-12-12 15:11:48', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1186', '1440', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 13:16:03', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1187', '1441', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 13:37:11', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1188', '1442', '0001010203010000', '45方钢', 'M00005', '100.000', '0', null, '2018-12-12 13:38:58', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1189', '1443', '0001010203010000', '45方钢', 'M00005', '100.000', '0', null, '2018-12-12 13:39:40', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1190', '1444', '0001010203010000', '45方钢', 'M00010', '1000.000', '0', null, '2018-12-12 13:47:53', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1191', '1445', '0001010203010000', '45方钢', 'M00010', '1000.000', '0', null, '2018-12-12 13:48:13', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1192', '1446', '6301003400000000', '等离子火焰切割机', 'M00369', '200.000', '130', null, '2018-12-12 14:05:37', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1193', '1447', '6301003400000000', '等离子火焰切割机', 'M00367', '20.000', '50', null, '2018-12-12 14:05:38', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1194', '1449', '0001010101004000', 'A3元钢', 'M00001', '100.000', '0', null, '2018-12-12 14:28:46', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1195', '1449', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 14:28:46', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1196', '1450', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 14:30:06', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1197', '1452', '5000000000000028', 'LNG分子筛包环缝专机', 'M00378', '50.000', '65', null, '2018-12-12 14:43:15', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1198', '1453', '5000000000000028', 'LNG分子筛包环缝专机', 'L00320', '50.000', '65', null, '2018-12-12 14:43:08', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1199', '1454', '5000000000000028', 'LNG分子筛包环缝专机', 'S00021', '100.000', '130', null, '2018-12-12 14:42:53', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1200', '1455', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 14:43:41', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1201', '1458', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 14:46:50', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1202', '1459', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 14:48:24', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1209', '1466', '5000000000000028', 'LNG分子筛包环缝专机', 'M00378', '50.000', '65', null, '2018-12-12 15:12:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1210', '1467', '5000000000000028', 'LNG分子筛包环缝专机', 'L00320', '50.000', '65', null, '2018-12-12 15:12:02', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1211', '1468', '5000000000000028', 'LNG分子筛包环缝专机', 'S00021', '100.000', '130', null, '2018-12-12 15:12:03', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1212', '1470', '0008030301019000', '外部轴电源接口X7.1(+1轴) ', 'M00367', '100.000', '883', null, '2018-12-12 15:19:16', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1213', '1471', '2102041320113700', '散热器', 'M00374', '100.000', '2234', null, '2018-12-12 15:22:58', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1214', '1472', '2102041320113700', '散热器', 'M00373', '100.000', '676', null, '2018-12-12 15:23:00', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1215', '1473', '2102041320113700', '散热器', 'M00372', '100.000', '6575', null, '2018-12-12 15:23:02', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1216', '1474', '2102041320113700', '散热器', 'M00369', '100.000', '655', null, '2018-12-12 15:23:05', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1217', '1476', '0001010203010000', '45方钢', 'M00004', '100.000', '0', null, '2018-12-12 15:24:31', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1218', '1477', '0001010203010000', '45方钢', 'M00004', '100.000', '0', null, '2018-12-12 15:24:56', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1219', '1479', '2102041320113700', '散热器', 'M00369', '100.000', '655', null, '2018-12-12 15:29:20', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1220', '1480', '0001010203010000', '45方钢', 'M00003', '100.000', '0', null, '2018-12-12 18:47:14', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1221', '1481', '0001010102006000', '45元钢', 'M00002', '100.000', '0', null, '2018-12-12 18:57:56', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1223', '1486', '9800000000000006', '管、缆、线部件借用件', 'M00377', '100.000', '200', null, '2018-12-12 19:18:02', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1224', '1487', '6000000000000143', '低温气瓶外胆环缝TIG', 'M00404', '20.000', '23', null, '2018-12-12 19:20:35', 'ricard', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1225', '1488', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 19:25:10', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1226', '1490', '0001010203010000', '45方钢', 'M00002', '100.000', '0', null, '2018-12-12 19:25:15', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1227', '1491', '0001010203010000', '45方钢', 'M00001', '100.000', '0', null, '2018-12-12 19:41:55', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1228', '1493', '6301003400000000', '等离子火焰切割机', 'S00018', '100.000', '130', null, '2018-12-13 08:49:23', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1229', '1494', '6000000000000680', '油缸自动焊接系统', 'L00320', '200.000', '130', null, '2018-12-13 08:49:39', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1230', '1495', '6000000000000680', '油缸自动焊接系统', 'M00379', '50.000', '100', null, '2018-12-13 09:25:45', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1231', '1496', '6000000000000680', '油缸自动焊接系统', 'M00378', '100.000', '200', null, '2018-12-13 09:25:55', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1232', '1497', '6000000000000680', '油缸自动焊接系统', 'M00376', '60.000', '120', null, '2018-12-13 09:26:00', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1233', '1498', '6000000000000143', '低温气瓶外胆环缝TIG', 'S00021', '300.000', '6000', null, '2018-12-13 09:26:06', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1234', '1499', '6000000000000680', '油缸自动焊接系统', 'M00380', '200.000', '600', null, '2018-12-13 09:29:40', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1235', '1500', '6000000000000680', '油缸自动焊接系统', 'M00376', '60.000', '120', null, '2018-12-13 09:36:07', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1236', '1501', '6000000000000680', '油缸自动焊接系统', 'M00378', '100.000', '200', null, '2018-12-13 09:36:08', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1237', '1502', '6000000000000680', '油缸自动焊接系统', 'M00379', '50.000', '100', null, '2018-12-13 09:36:09', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1238', '1503', '6000000000000680', '油缸自动焊接系统', 'M00380', '200.000', '600', null, '2018-12-13 09:36:10', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1239', '1504', '2102041320113700', '散热器', 'M00373', '100.000', '676', null, '2018-12-13 09:45:28', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1240', '1514', '6301003400000000', '等离子火焰切割机', 'S00015', '200.000', '400', null, '2018-12-14 08:57:29', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1241', '1515', '9800000000000008', '旋转通用部件', 'S00019', '200.000', '420', null, '2018-12-14 08:57:23', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1242', '1517', '0001010203010000', '45方钢', 'M00015', '100.000', '0', null, '2018-12-17 17:05:54', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1243', '1518', '0001010203010000', '45方钢', 'M00016', '1.000', '0', null, '2018-12-25 15:55:40', 'tony', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1244', '1519', '0001010203010000', '45方钢', 'M00018', '100.000', '0', null, '2018-12-25 15:56:26', 'tony', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1245', '1520', '0001010203010000', '45方钢', 'M00020', '100.000', '0', null, '2018-12-25 15:55:54', 'tony', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1246', '1521', '0001010203010000', '45方钢', 'M00030', '100.000', '0', null, '2018-12-25 15:56:22', 'tony', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1247', '1522', '0001010203010000', '45方钢', 'M00031', '50.000', '0', null, '2018-12-25 15:56:19', 'tony', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1248', '1523', '0001010203010000', '45方钢', 'M00110', '100.000', '0', null, '2018-12-25 15:56:15', 'tony', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1249', '1524', '0001010203010000', '45方钢', 'M00111', '100.000', '0', null, '2018-12-25 15:56:12', 'tony', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1250', '1525', '0001010203010000', '45方钢', 'M00123', '100.000', '0', null, '2018-12-25 15:56:08', 'tony', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1251', '1526', '0001010203010000', '45方钢', 'M00060', '100.000', '50', null, '2018-12-17 19:25:40', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1252', '1527', '1000', '测试物料', 'M00200', '100.000', '100', null, '2018-12-17 19:50:04', 'abc', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1253', '1528', '0001010203010000', '45方钢', 'M00019', '100.000', '50', null, '2018-12-18 08:55:55', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1254', '1529', '0001010203010000', '45方钢', 'M00004', '100.000', '0', null, '2018-12-25 15:56:05', 'tony', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1255', '1530', '9800000000000015', 'TC36总装图借用部件', 'M00382', '200.000', '0', null, '2018-12-18 11:50:16', 'my test', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1256', '1531', '2102041320113700', '散热器', 'M00385', '300.000', '250', null, '2018-12-18 11:49:58', 'my test', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1257', '1533', '9800000000000015', 'TC36总装图借用部件', 'M00382', '200.000', '0', null, '2018-12-18 16:34:16', 'my test', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1258', '1534', '2102041320113700', '散热器', 'M00385', '300.000', '250', null, '2018-12-18 16:34:19', 'my test', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1259', '1548', '2102041320113700', '散热器', 'M00356', '100.000', '88', null, '2018-12-24 08:48:12', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1260', '1549', '2102041320113700', '散热器', 'M00355', '100.000', '98', null, '2018-12-24 08:48:15', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1261', '1550', '2102041320113700', '散热器', 'M00354', '100.000', '76', null, '2018-12-24 08:48:17', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1262', '1551', '2102041320113700', '散热器', 'M00353', '100.000', '76', null, '2018-12-24 08:48:20', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1263', '1552', '2102041320113700', '散热器', 'M00352', '100.000', '67', null, '2018-12-24 08:48:22', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1264', '1553', '2102041320113700', '散热器', 'M00351', '100.000', '88', null, '2018-12-24 08:48:25', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1265', '1554', '9800000000000015', 'TC36总装图借用部件', 'M00384', '200.000', '0', null, '2018-12-24 08:48:27', 'xqs', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1266', '1609', '9800000000000006', '管、缆、线部件借用件', 'M00401', '100.000', '210', null, '2018-12-25 15:16:10', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1267', '1610', '9800000000000007', 'TP040管板焊头外构件', 'M00400', '200.000', '260', null, '2018-12-25 15:16:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1268', '1611', '9800000000000007', 'TP040管板焊头外构件', 'M00400', '200.000', '260', null, '2018-12-25 15:22:06', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1269', '1612', '9800000000000006', '管、缆、线部件借用件', 'M00377', '100.000', '200', null, '2018-12-25 15:22:07', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1270', '1613', '2102041320113700', '散热器', 'M00372', '100.000', '6575', null, '2018-12-25 15:27:32', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1271', '1622', '9800000000000006', '管、缆、线部件借用件', 'M00396', '100.000', '156', null, '2018-12-26 11:51:05', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1272', '1623', '0001010203010000', '45方钢', 'M00400', '120.000', '150', null, '2018-12-26 11:50:59', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1273', '1624', '6000000000000195', '低温气瓶内胆环缝焊接系统', 'M00399', '200.000', '210', null, '2018-12-26 11:50:52', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1274', '1625', '0001010203010000', '45方钢', 'M00019', '100.000', '50', null, '2018-12-26 11:56:02', 'Quartz', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1275', '1626', '9800000000000006', '管、缆、线部件借用件', 'M00401', '100.000', '210', null, '2018-12-26 11:56:05', 'Quartz', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1276', '1627', '0001010203010000', '45方钢', 'M00020', '100.000', '0', null, '2018-12-26 12:02:02', 'Quartz', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1277', '1628', '0001010203010000', '45方钢', 'M00004', '100.000', '0', null, '2018-12-26 13:34:23', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1278', '1629', '6000000000000195', '低温气瓶内胆环缝焊接系统', 'M00399', '200.000', '210', null, '2018-12-26 13:36:40', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1279', '1630', '2102041320113700', '散热器', 'M00399', '300.000', '260', null, '2018-12-26 13:41:17', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1280', '1634', '0001010203010000', '45方钢', 'M00123', '100.000', '0', null, '2018-12-26 14:22:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1281', '1635', '0001010203010000', '45方钢', 'M00110', '100.000', '0', null, '2018-12-26 14:21:20', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1282', '1637', '6000000000000195', '低温气瓶内胆环缝焊接系统', 'M00401', '100.000', '150', null, '2018-12-27 08:50:53', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1283', '1638', '0001010203010000', '45方钢', 'M00393', '200.000', '0', null, '2018-12-27 08:51:00', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1284', '1639', '6000000000000195', '低温气瓶内胆环缝焊接系统', 'M00401', '100.000', '150', null, '2018-12-27 08:54:55', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1285', '1642', '0001010203010000', '45方钢', 'M00015', '80.000', '0', null, '2018-12-27 09:12:02', 'Quartz', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1286', '1643', '0001010203010000', '45方钢', 'M00016', '1.000', '0', null, '2018-12-27 11:08:51', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1287', '1645', '9800000000000000', '必备附件借用件', 'M00401', '200.000', '500', null, '2018-12-29 17:14:22', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1288', '1646', '0001010203010000', '45方钢', 'M00031', '50.000', '0', null, '2018-12-29 17:17:01', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1289', '1649', '5200000000000033', '2-8”定长切割坡口一体机', 'M00335', '200.000', '160', null, '2019-01-02 08:53:12', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1290', '1650', '3600001702001600', '导轨支撑腿', 'M00336', '200.000', '200', null, '2019-01-02 08:53:07', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1291', '1651', '9800000000000008', '旋转通用部件', 'S00019', '200.000', '420', null, '2019-01-02 09:00:15', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1292', '1655', '5200000000000077', '426端面坡口机系统', 'M00376', '100.000', '630', null, '2019-01-03 16:22:49', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1293', '1656', '6000000000000150', '边梁P+T自动焊接系统', 'S00019', '200.000', '62', null, '2019-01-03 16:22:42', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1294', '1657', '5200000000000077', '426端面坡口机系统', 'M00376', '100.000', '630', null, '2019-01-03 16:25:05', 'wjj', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1295', '1659', '123456', '测试', 'M00004', '100.000', '0', null, '2019-01-04 09:02:08', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1296', '1660', '123456', '测试', 'M00005', '100.000', '0', null, '2019-01-04 09:02:28', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1297', '1661', '123456', '测试', 'M00006', '100.000', '0', null, '2019-01-04 09:02:45', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1298', '1662', '123456', '测试', 'M00007', '100.000', '0', null, '2019-01-04 09:09:10', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1299', '1663', '123456', '测试', 'M00008', '100.000', '0', null, '2019-01-04 09:09:13', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1300', '1664', '123456', '测试', 'M00009', '100.000', '0', null, '2019-01-04 09:09:16', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1301', '1665', '123456', '测试', 'M00010', '100.000', '0', null, '2019-01-04 09:09:22', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1302', '1666', '123456', '测试', 'M00011', '100.000', '0', null, '2019-01-04 09:09:29', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1303', '1667', '123456', '测试', 'M00012', '100.000', '0', null, '2019-01-04 09:10:33', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1304', '1668', '123456', '测试', 'M00013', '100.000', '0', null, '2019-01-04 14:03:12', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1305', '1669', '123456', '测试', 'M00014', '100.000', '0', null, '2019-01-04 14:57:33', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1306', '1670', '123456', '测试', 'M00016', '100.000', '0', null, '2019-01-04 14:57:39', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1307', '1671', '123456', '测试', 'M00012', '100.000', '0', null, '2019-01-04 15:18:56', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1308', '1672', '123456', '测试', 'M00013', '100.000', '0', null, '2019-01-04 15:19:41', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1309', '1673', '123456', '测试', 'M00014', '100.000', '0', null, '2019-01-04 15:20:40', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1310', '1674', '123456', '测试', 'M00016', '100.000', '0', null, '2019-01-04 15:20:57', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1311', '1675', '123456', '测试', 'M00004', '100.000', '0', null, '2019-01-04 15:21:11', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1312', '1676', '123456', '测试', 'M00005', '100.000', '0', null, '2019-01-04 15:21:22', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1313', '1677', '123456', '测试', 'M00006', '100.000', '0', null, '2019-01-04 15:21:48', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1314', '1678', '123456', '测试', 'M00007', '100.000', '0', null, '2019-01-04 15:22:11', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1315', '1679', '123456', '测试', 'M00008', '100.000', '0', null, '2019-01-04 15:22:33', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1316', '1680', '123456', '测试', 'M00009', '100.000', '0', null, '2019-01-04 15:22:47', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1317', '1681', '123456', '测试', 'M00010', '100.000', '0', null, '2019-01-04 15:23:01', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1318', '1682', '123456', '测试', 'M00011', '100.000', '0', null, '2019-01-04 15:23:25', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1319', '1683', '123456', '测试', 'M00020', '100.000', '0', null, '2019-01-04 15:36:49', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1320', '1684', '123456', '测试', 'M00021', '100.000', '0', null, '2019-01-04 15:36:53', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1321', '1685', '123456', '测试', 'M00020', '100.000', '0', null, '2019-01-04 15:37:17', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1322', '1686', '123456', '测试', 'M00022', '100.000', '0', null, '2019-01-04 15:49:47', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1323', '1687', '123456', '测试', 'M00023', '100.000', '0', null, '2019-01-04 15:49:51', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1324', '1688', '123456', '测试', 'M00025', '100.000', '0', null, '2019-01-04 15:49:55', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1325', '1689', '123456', '测试', 'M00026', '100.000', '0', null, '2019-01-04 15:49:59', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1326', '1690', '123456', '测试', 'M00027', '100.000', '0', null, '2019-01-04 15:50:03', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1327', '1691', '123456', '测试', 'M00028', '100.000', '0', null, '2019-01-04 15:50:06', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1328', '1692', '123456', '测试', 'M00029', '100.000', '0', null, '2019-01-04 15:50:10', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1329', '1693', '123456', '测试', 'M00031', '100.000', '0', null, '2019-01-04 15:50:13', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1330', '1694', '123456', '测试', 'M00032', '100.000', '0', null, '2019-01-04 15:50:18', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1331', '1695', '123456', '测试', 'M00034', '100.000', '0', null, '2019-01-04 15:50:21', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1332', '1696', '123456', '测试', 'M00037', '100.000', '0', null, '2019-01-04 15:50:24', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1333', '1697', '123456', '测试', 'M00038', '100.000', '0', null, '2019-01-04 15:50:27', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1334', '1698', '123456', '测试', 'M00039', '100.000', '0', null, '2019-01-04 15:50:31', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1335', '1699', '123456', '测试', 'M00040', '100.000', '0', null, '2019-01-04 15:50:34', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1336', '1700', '123456', '测试', 'M00041', '100.000', '0', null, '2019-01-04 15:50:38', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1337', '1701', '123456', '测试', 'M00042', '100.000', '0', null, '2019-01-04 15:51:39', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1338', '1702', '123456', '测试', 'M00043', '100.000', '0', null, '2019-01-04 15:51:42', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1339', '1703', '123456', '测试', 'M00044', '100.000', '0', null, '2019-01-04 15:51:46', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1340', '1704', '123456', '测试', 'M00045', '100.000', '0', null, '2019-01-04 15:51:49', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1341', '1705', '123456', '测试', 'M00047', '100.000', '0', null, '2019-01-04 15:51:53', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1342', '1706', '123456', '测试', 'M00048', '100.000', '0', null, '2019-01-04 15:52:05', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1343', '1707', '123456', '测试', 'M00049', '100.000', '0', null, '2019-01-04 15:52:14', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1344', '1708', '123456', '测试', 'M00050', '100.000', '0', null, '2019-01-04 16:43:56', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1345', '1709', '123456', '测试', 'M00051', '100.000', '0', null, '2019-01-04 16:50:40', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1346', '1710', '123456', '测试', 'M00052', '100.000', '0', null, '2019-01-04 16:50:45', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1347', '1711', '123456', '测试', 'M00025', '100.000', '0', null, '2019-01-04 17:12:29', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1348', '1712', '123456', '测试', 'M00026', '100.000', '0', null, '2019-01-04 17:13:19', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1349', '1713', '123456', '测试', 'M00027', '100.000', '0', null, '2019-01-04 17:20:05', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1350', '1714', '123456', '测试', 'M00061', '100.000', '0', null, '2019-01-04 17:22:29', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1351', '1715', '123456', '测试', 'M00062', '100.000', '0', null, '2019-01-04 17:22:35', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1352', '1716', '123456', '测试', 'M00063', '100.000', '0', null, '2019-01-04 17:22:38', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1353', '1717', '123456', '测试', 'M00022', '100.000', '0', null, '2019-01-04 18:11:52', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1354', '1719', '123456', '测试', 'M00028', '100.000', '0', null, '2019-01-04 18:12:16', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1355', '1720', '123456', '测试', 'M00029', '100.000', '0', null, '2019-01-04 18:13:18', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1356', '1721', '123456', '测试', 'M00031', '100.000', '0', null, '2019-01-04 18:13:48', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1357', '1722', '123456', '测试', 'M00072', '100.000', '0', null, '2019-01-04 18:16:26', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1358', '1723', '123456', '测试', 'M00073', '100.000', '0', null, '2019-01-04 18:16:30', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1359', '1724', '123456', '测试', 'M00074', '100.000', '0', null, '2019-01-04 18:16:36', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1360', '1725', '123456', '测试', 'M00075', '100.000', '0', null, '2019-01-04 18:16:39', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1361', '1726', '123456', '测试', 'M00076', '100.000', '0', null, '2019-01-04 18:16:44', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1362', '1727', '123456', '测试', 'M00032', '100.000', '0', null, '2019-01-04 18:24:53', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1363', '1728', '123456', '测试', 'M00034', '100.000', '0', null, '2019-01-04 18:49:49', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1364', '1729', '123456', '测试', 'M00037', '100.000', '0', null, '2019-01-04 18:53:42', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1365', '1730', '123456', '测试', 'M00038', '100.000', '0', null, '2019-01-04 18:55:28', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1366', '1731', '123456', '测试', 'M00081', '100.000', '0', null, '2019-01-04 18:59:39', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1367', '1732', '123456', '测试', 'M00082', '100.000', '0', null, '2019-01-04 18:59:44', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1368', '1733', '123456', '测试', 'M00083', '100.000', '0', null, '2019-01-04 18:59:49', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1369', '1734', '123456', '测试', 'M00039', '100.000', '0', null, '2019-01-04 19:00:47', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1370', '1735', '123456', '测试', 'M00040', '100.000', '0', null, '2019-01-04 19:07:35', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1371', '1736', '123456', '测试', 'M00041', '100.000', '0', null, '2019-01-04 19:09:48', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1372', '1737', '123456', '测试', 'M00042', '100.000', '0', null, '2019-01-04 19:12:08', 'youjie', '\0');
INSERT INTO `wcstaskdetail_deleted` VALUES ('1373', '1738', '123456', '测试', 'M00043', '100.000', '0', null, '2019-01-04 19:12:45', 'youjie', '\0');

-- ----------------------------
-- Table structure for wcstask_deleted
-- ----------------------------
DROP TABLE IF EXISTS `wcstask_deleted`;
CREATE TABLE `wcstask_deleted` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `remoteTaskNo` varchar(50) NOT NULL DEFAULT '0',
  `priority` smallint(6) NOT NULL DEFAULT '10' COMMENT '优先级(1-99)',
  `taskType` smallint(6) NOT NULL COMMENT '任务类型',
  `containerCode` varchar(50) DEFAULT NULL COMMENT '容器编号',
  `port` int(11) NOT NULL COMMENT '出入口',
  `fromLocationCode` varchar(50) DEFAULT NULL COMMENT '源库位',
  `toLocationCode` varchar(50) DEFAULT NULL COMMENT '目的库位',
  `taskStatus` smallint(6) NOT NULL DEFAULT '1' COMMENT '首状态',
  `stage` int(11) NOT NULL DEFAULT '1',
  `arriveEquipmentCode` varchar(255) DEFAULT NULL,
  `isEmptyOut` int(11) NOT NULL DEFAULT '0' COMMENT '新增、是否空出标记',
  `isDoubleIn` int(11) NOT NULL DEFAULT '0' COMMENT '新增、是否重入处理标志',
  `doubleInLocationCode` varchar(50) DEFAULT NULL COMMENT '重入时，重新写入的去向地址',
  `sendAgain` int(11) DEFAULT '0' COMMENT '重新下发给堆垛机，1表示重新下发，2表示已经响应重新下发',
  `warehouseCode` varchar(50) DEFAULT NULL COMMENT '仓库',
  `created` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `createdBy` varchar(50) NOT NULL COMMENT '任务下达人',
  `updated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最后修改时间',
  `updatedBy` varchar(50) DEFAULT NULL COMMENT '更新用户',
  `deleted` bit(1) NOT NULL DEFAULT b'0' COMMENT '删除标记',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1739 DEFAULT CHARSET=utf8mb4 COMMENT='立库任务表';

-- ----------------------------
-- Records of wcstask_deleted
-- ----------------------------
INSERT INTO `wcstask_deleted` VALUES ('1387', '0', '5', '100', 'M00382', '0', '', 'L04-08-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 18:43:28', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1388', '0', '5', '100', 'M00405', '0', '', 'L04-06-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 18:43:35', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1389', '0', '5', '100', 'M00404', '0', '', 'L04-06-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 18:43:38', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1390', '0', '10', '100', 'M00001', '1006', '', 'L01-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 18:44:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1392', '0', '10', '700', 'M00001', '1001', 'L01-14-01', 'L01-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 18:53:53', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1393', '0', '10', '600', 'M00001', '1001', 'L01-14-01', null, '100', '1', null, '1', '0', null, '0', 'XT0001', '2018-12-11 19:07:22', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1394', '0', '10', '300', 'M00404', '1012', 'L04-06-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:20:03', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1395', '0', '10', '300', 'M00382', '1012', 'L04-08-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:20:05', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1396', '0', '10', '100', 'M00001', '1006', '', 'L01-01-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:24:43', 'youjie', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1397', '0', '5', '100', 'M00399', '0', '', 'L04-06-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:56', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1398', '0', '5', '100', 'M00398', '0', '', 'L04-06-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:57', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1399', '0', '5', '100', 'M00397', '0', '', 'L04-06-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:58', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1400', '0', '5', '100', 'M00395', '0', '', 'L04-05-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:58', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1401', '0', '5', '100', 'M00394', '0', '', 'L04-10-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:25:59', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1402', '0', '5', '100', 'M00393', '0', '', 'L04-10-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:26:00', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1403', '0', '5', '100', 'M00392', '0', '', 'L04-10-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:26:02', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1404', '0', '5', '100', 'M00390', '0', '', 'L04-10-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:26:03', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1405', '0', '5', '100', 'M00391', '0', '', 'L04-10-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:26:04', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1407', '0', '10', '700', 'M00002', '1001', 'L01-01-02', 'L01-01-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-11 19:39:44', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1408', '0', '10', '700', 'M00391', '1012', 'L04-10-02', 'L04-10-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 08:46:33', 'xqs', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1409', '0', '10', '300', 'M00390', '1012', 'L04-10-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 08:48:57', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1410', '0', '10', '300', 'M00405', '1012', 'L04-06-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 08:48:58', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1411', '0', '10', '300', 'M00395', '1012', 'L04-05-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 08:48:59', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1412', '0', '10', '300', 'M00399', '1012', 'L04-06-02', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 08:49:00', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1413', '0', '10', '300', 'M00398', '1012', 'L04-06-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 08:49:02', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1414', '0', '5', '100', 'M00370', '0', '', 'L04-13-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:32:01', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1415', '0', '5', '100', 'M00368', '0', '', 'L04-13-01', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:32:02', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1416', '0', '10', '100', 'M00003', '1006', '', 'L04-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:38:11', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1417', '0', '5', '100', 'M00405', '0', '', 'L04-14-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:33', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1418', '0', '5', '100', 'M00404', '0', '', 'L04-14-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:33', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1419', '0', '5', '100', 'M00403', '0', '', 'L04-14-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:33', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1420', '0', '5', '200', 'M00003', '0', 'L04-14-01', 'L04-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:54', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1421', '0', '5', '100', 'M00367', '0', '', 'L04-13-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:56', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1422', '0', '5', '100', 'M00369', '0', '', 'L04-13-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:44:57', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1423', '0', '10', '300', 'M00393', '1006', 'L04-10-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:46:45', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1424', '0', '10', '300', 'M00397', '1001', 'L04-06-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 09:46:46', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1425', '0', '5', '500', 'S00020', '1012', null, 'L01-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 10:12:46', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1426', '0', '5', '500', 'S00021', '1012', null, 'L01-14-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 10:16:43', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1427', '0', '10', '600', 'S00020', '1012', 'L01-14-01', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 10:19:49', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1428', '0', '10', '600', 'S00021', '1012', 'L01-14-02', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 10:25:02', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1429', '0', '5', '500', 'M00359', '1012', null, 'L01-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 10:26:43', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1430', '0', '10', '300', 'M00370', '1006', 'L04-13-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 10:30:05', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1432', '0', '5', '500', 'M00339', '1012', null, 'L04-12-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 10:38:53', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1433', '0', '5', '500', 'M00335', '1012', null, 'L01-10-01', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 10:39:20', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1434', '0', '5', '500', 'S00022', '1012', null, 'L04-13-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 10:51:51', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1435', '0', '5', '500', 'S00019', '1012', null, 'L01-14-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 11:09:37', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1436', '0', '5', '500', 'S00020', '1012', null, 'L04-08-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 11:10:33', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1437', '0', '10', '600', 'M00359', '1012', 'L01-14-01', null, '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 11:13:33', 'wjj', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1438', '0', '5', '200', 'M00391', '0', 'L04-10-02', 'L04-10-02', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 12:06:42', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1439', '0', '5', '100', 'M00402', '0', '', 'L04-10-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 12:06:43', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1440', '0', '10', '300', 'M00001', '1012', 'L01-01-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 13:16:02', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1441', '0', '10', '300', 'M00003', '1001', 'L04-14-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 13:37:12', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1442', '0', '10', '100', 'M00005', '1006', '', 'L01-01-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 13:38:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1443', '0', '10', '300', 'M00005', '1012', 'L01-01-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 13:39:40', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1444', '0', '10', '100', 'M00010', '1006', '', 'L02-01-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 13:47:13', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1445', '0', '10', '300', 'M00010', '1012', 'L02-01-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 13:48:12', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1446', '0', '10', '300', 'M00369', '1006', 'L04-13-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:05:38', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1447', '0', '10', '300', 'M00367', '1006', 'L04-13-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:05:39', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1448', '0', '10', '600', 'M00002', '1012', 'L01-01-02', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:10:37', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1449', '0', '10', '100', 'M00001', '1006', '', 'L01-01-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:23:14', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1450', '0', '10', '300', 'M00001', '1012', 'L01-01-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:30:06', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1451', '0', '10', '700', 'M00002', '1012', 'L01-01-02', 'L01-01-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:39:24', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1452', '0', '5', '100', 'M00378', '0', '', 'L04-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:41:54', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1453', '0', '5', '100', 'L00320', '0', '', 'L04-13-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:41:55', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1454', '0', '5', '100', 'S00021', '0', '', 'L04-13-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:41:56', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1455', '0', '10', '100', 'M00003', '1006', '', 'L01-01-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:43:22', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1456', '0', '10', '700', 'M00003', '1012', 'L01-01-03', 'L01-01-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:44:11', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1457', '0', '10', '600', 'M00003', '1012', 'L01-01-03', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:45:36', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1458', '0', '10', '100', 'M00003', '1006', '', 'L01-01-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:46:34', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1459', '0', '10', '300', 'M00003', '1012', 'L01-01-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 14:48:23', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1466', '0', '10', '300', 'M00378', '1012', 'L04-14-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:12:02', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1467', '0', '10', '300', 'L00320', '1012', 'L04-13-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:12:03', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1468', '0', '10', '300', 'S00021', '1012', 'L04-13-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:12:03', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1469', '0', '10', '900', 'S00022', '1012', 'L04-13-03', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:15:31', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1470', '0', '5', '100', 'M00367', '0', '', 'L04-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:18:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1471', '0', '5', '100', 'M00374', '0', '', 'L01-14-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:22:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1472', '0', '5', '100', 'M00373', '0', '', 'L01-14-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:22:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1473', '0', '5', '100', 'M00372', '0', '', 'L01-14-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:22:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1474', '0', '5', '100', 'M00369', '0', '', 'L04-13-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:22:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1475', '0', '10', '700', 'M00374', '1012', 'L01-14-03', 'L01-14-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:23:39', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1476', '0', '10', '100', 'M00004', '0', '', 'L04-13-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:24:03', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1477', '0', '10', '300', 'M00004', '1012', 'L04-13-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:24:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1478', '0', '10', '600', 'M00374', '1012', 'L01-14-03', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:25:29', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1479', '0', '10', '300', 'M00369', '1006', 'L04-13-04', '', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 15:28:58', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1480', '0', '10', '100', 'M00003', '0', '', 'L02-01-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 18:46:42', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1481', '0', '10', '300', 'M00002', '1012', 'L01-01-02', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 18:57:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1482', '0', '15', '800', 'M00003', '1012', 'L02-01-02', 'L04-01-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 18:57:56', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1484', '0', '10', '700', 'M00003', '1012', 'L04-01-01', 'L04-01-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 19:11:24', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1485', '0', '10', '600', 'M00003', '1012', 'L04-01-01', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 19:14:04', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1486', '0', '5', '100', 'M00377', '0', '', 'L03-14-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 19:17:53', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1487', '0', '10', '300', 'M00404', '1012', 'L04-14-03', '', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 19:20:04', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1488', '0', '10', '100', 'M00001', '0', '', 'L01-01-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 19:20:18', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1489', '0', '15', '800', 'M00377', '1012', 'L03-14-03', 'L02-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 19:20:35', 'ricard', '2019-08-15 17:20:08', 'ricard', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1490', '0', '10', '100', 'M00002', '0', '', 'L02-01-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 19:20:51', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1491', '0', '10', '300', 'M00001', '1012', 'L01-01-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 19:41:53', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1492', '0', '15', '800', 'M00002', '1012', 'L02-01-01', 'L04-01-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-12 19:41:54', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1493', '0', '5', '100', 'S00018', '0', '', 'L04-08-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 08:48:06', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1494', '0', '5', '100', 'L00320', '0', '', 'L04-07-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 08:48:07', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1495', '0', '5', '100', 'M00379', '0', '', 'L04-07-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:25:14', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1496', '0', '5', '100', 'M00378', '0', '', 'L04-07-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:25:15', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1497', '0', '5', '100', 'M00376', '0', '', 'L04-07-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:25:16', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1498', '0', '5', '100', 'S00021', '0', '', 'L04-13-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:25:16', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1499', '0', '5', '100', 'M00380', '0', '', 'L04-07-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:29:29', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1500', '0', '10', '300', 'M00376', '1006', 'L04-07-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:36:09', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1501', '0', '10', '300', 'M00378', '1006', 'L04-07-02', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:36:10', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1502', '0', '10', '300', 'M00379', '1006', 'L04-07-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:36:11', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1503', '0', '10', '300', 'M00380', '1006', 'L04-07-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:36:11', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1504', '0', '10', '300', 'M00373', '1012', 'L01-14-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:45:30', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1505', '0', '5', '500', 'M00374', '1012', null, 'L01-14-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:49:40', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1506', '0', '5', '500', 'M00375', '1012', null, 'L04-08-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:51:47', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1507', '0', '10', '600', 'S00019', '1012', 'L01-14-02', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 09:53:18', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1508', '0', '10', '700', 'M00379', '1012', 'L04-09-02', 'L04-09-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 10:00:34', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1509', '0', '10', '700', 'M00380', '1012', 'L04-09-03', 'L04-09-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 10:05:38', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1510', '0', '10', '700', 'M00386', '1012', 'L04-08-04', 'L04-08-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 10:14:21', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1511', '0', '10', '900', 'M00002', '1012', 'L04-01-01', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 10:19:27', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1512', '0', '10', '700', 'M00002', '1012', 'L04-01-01', 'L04-01-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 10:25:02', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1513', '0', '10', '700', 'M00395', '1012', 'L04-02-05', 'L04-02-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-13 11:34:18', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1514', '0', '5', '100', 'S00015', '0', '', 'L04-09-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-14 08:57:09', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1515', '0', '5', '100', 'S00019', '0', '', 'L04-09-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-14 08:57:10', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1516', '0', '10', '700', 'S00021', '1012', 'L04-13-05', 'L04-13-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 15:39:47', 'xqs', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1517', '0', '10', '100', 'M00015', '1006', '', 'L01-14-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 17:04:46', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1518', '0', '10', '100', 'M00016', '1006', '', 'L01-14-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 17:09:22', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1519', '0', '10', '100', 'M00018', '1006', '', 'L04-11-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 17:12:13', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1520', '0', '10', '100', 'M00020', '1006', '', 'L01-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 17:18:36', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1521', '0', '10', '100', 'M00030', '1006', '', 'L04-11-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 17:20:38', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1522', '0', '10', '100', 'M00031', '1006', '', 'L04-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 17:21:38', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1523', '0', '10', '100', 'M00110', '1006', '', 'L01-12-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 19:12:18', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1524', '0', '10', '100', 'M00111', '1006', '', 'L01-12-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 19:19:12', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1525', '0', '10', '100', 'M00123', '1006', '', 'L01-12-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 19:22:29', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1526', '0', '10', '100', 'M00060', '1006', '', 'L01-14-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 19:24:10', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1527', '0', '5', '100', 'M00200', '0', '', 'L03-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-17 19:49:51', 'abc', '2019-08-15 17:20:08', 'abc', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1528', '0', '10', '100', 'M00019', '1006', '', 'L01-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 08:54:27', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1529', '0', '10', '100', 'M00004', '1006', '', 'L01-12-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 09:02:41', 'youjie', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1530', '0', '5', '100', 'M00382', '0', '', 'L01-13-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 11:47:21', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1531', '0', '5', '100', 'M00385', '0', '', 'L04-07-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 11:47:23', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1532', '0', '0', '100', 'M00002', '1012', '', 'L02-09-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 14:56:59', 'admin', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1533', '0', '10', '300', 'M00382', '1006', 'L01-13-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 16:34:17', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1534', '0', '10', '300', 'M00385', '1006', 'L04-07-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 16:34:20', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1535', '0', '10', '700', 'S00015', '1012', 'L04-09-04', 'L04-09-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 16:59:54', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1536', '0', '10', '700', 'M00397', '1012', 'L04-07-04', 'L04-07-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 17:05:50', 'my test', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1537', '0', '5', '500', 'M00398', '1012', null, 'L04-07-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 17:21:29', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1538', '0', '10', '600', 'S00020', '1012', 'L04-08-03', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-18 17:24:16', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1539', '0', '10', '600', 'M00398', '1012', 'L04-07-02', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-19 09:57:29', 'my test', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1540', '0', '5', '500', 'S00020', '1012', null, 'L01-13-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-19 10:07:53', 'my test', '2019-08-15 17:20:08', 'my test', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1541', '0', '10', '700', 'S00019', '1012', 'L04-09-05', 'L04-09-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-21 11:04:14', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1542', '0', '10', '700', 'S00015', '1012', 'L04-09-04', 'L04-09-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-21 11:05:47', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1543', '0', '10', '700', 'M00015', '1012', 'L01-14-02', 'L01-14-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-21 11:09:01', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1544', '0', '10', '700', 'M00060', '1012', 'L01-14-04', 'L01-14-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-21 11:09:01', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1545', '0', '10', '700', 'M00019', '1012', 'L01-11-01', 'L01-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-21 11:09:01', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1546', '0', '10', '700', 'M00200', '1012', 'L03-14-01', 'L03-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-21 15:48:40', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1547', '0', '10', '700', 'M00019', '1012', 'L01-11-01', 'L01-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-21 15:49:09', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1548', '0', '5', '100', 'M00356', '0', '', 'L01-13-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1549', '0', '5', '100', 'M00355', '0', '', 'L01-13-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1550', '0', '5', '100', 'M00354', '0', '', 'L01-13-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1551', '0', '5', '100', 'M00353', '0', '', 'L01-13-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1552', '0', '5', '100', 'M00352', '0', '', 'L04-12-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:57', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1553', '0', '5', '100', 'M00351', '0', '', 'L04-12-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:58', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1554', '0', '5', '100', 'M00384', '0', '', 'L04-12-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 08:47:58', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1555', '0', '5', '500', 'M00005', '1012', null, 'L01-12-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 09:12:29', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1556', '0', '5', '500', 'M00006', '1012', null, 'L01-01-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 09:16:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1557', '0', '10', '700', 'M00352', '1006', 'L04-12-03', 'L04-12-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 09:42:07', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1558', '0', '10', '700', 'M00351', '1006', 'L04-12-04', 'L04-12-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 09:42:07', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1559', '0', '10', '700', 'M00384', '1006', 'L04-12-05', 'L04-12-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 09:42:07', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1560', '0', '10', '600', 'M00005', '1012', 'L01-12-02', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 09:43:32', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1561', '0', '10', '600', 'M00006', '1012', 'L01-01-05', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 09:50:44', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1562', '0', '5', '500', 'M00005', '1012', null, 'L01-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 10:10:56', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1563', '0', '5', '500', 'M00006', '1012', null, 'L01-12-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 11:01:52', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1564', '0', '5', '500', 'M00007', '1012', null, 'L01-12-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 11:02:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1565', '0', '10', '600', 'M00005', '1012', 'L01-12-01', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 11:06:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1566', '0', '5', '500', 'M00005', '1012', null, 'L01-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 11:16:58', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1567', '0', '10', '600', 'M00005', '1012', 'L01-12-01', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 11:24:54', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1568', '0', '5', '500', 'M00005', '1012', null, 'L01-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 11:25:25', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1569', '0', '10', '600', 'M00008', '1012', 'L01-12-01', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 11:26:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1570', '0', '10', '600', 'M00005', '1012', 'L01-12-02', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 11:52:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1571', '0', '10', '600', 'M00005', '1012', 'L01-12-03', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 11:58:26', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1572', '0', '5', '500', 'M00005', '1012', null, 'L01-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 12:02:29', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1573', '0', '10', '600', 'M00005', '1012', 'L01-12-01', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 12:10:14', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1574', '0', '10', '700', 'M00356', '1006', 'L01-13-02', 'L01-13-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 13:26:19', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1575', '0', '10', '700', 'M00354', '1006', 'L01-13-04', 'L01-13-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 13:26:19', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1576', '0', '10', '700', 'M00351', '1006', 'L04-12-04', 'L04-12-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 13:26:19', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1577', '0', '5', '500', 'M00005', '1012', null, 'L01-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:33:06', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1578', '0', '5', '500', 'M00006', '1012', null, 'L01-12-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:33:39', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1579', '0', '5', '500', 'M00007', '1012', null, 'L01-12-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:33:45', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1580', '0', '5', '500', 'M00008', '1012', null, 'L01-12-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:33:50', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1581', '0', '10', '600', 'M00005', '1012', 'L01-12-01', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:35:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1582', '0', '10', '600', 'M00007', '1012', 'L01-12-03', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:36:36', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1583', '0', '10', '600', 'M00006', '1012', 'L01-12-02', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:36:45', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1584', '0', '10', '600', 'M00008', '1012', 'L01-12-04', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:36:54', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1585', '0', '5', '500', 'M00005', '1012', null, 'L01-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:49:04', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1586', '0', '10', '600', 'M00005', '1012', 'L01-12-01', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 14:50:50', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1588', '0', '0', '800', 'M00367', '1012', 'L04-14-01', 'L04-11-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 18:24:25', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1589', '0', '0', '800', 'S00022', '1012', 'L04-13-03', 'L04-11-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 18:24:25', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1590', '0', '0', '800', 'M00367', '1012', 'L04-11-05', 'L04-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 18:43:25', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1591', '0', '0', '800', 'S00022', '1012', 'L04-11-04', 'L04-13-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 18:43:26', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1592', '0', '0', '800', 'M00367', '1012', 'L04-14-01', 'L04-11-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 18:52:23', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1593', '0', '0', '800', 'S00022', '1012', 'L04-13-03', 'L04-11-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 18:52:23', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1594', '0', '0', '800', 'M00367', '1012', 'L04-11-05', 'L04-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 19:32:00', 'superAdmin', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1595', '0', '0', '800', 'S00022', '1012', 'L04-11-04', 'L04-13-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-24 19:32:01', 'superAdmin', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1596', '0', '10', '700', 'M00355', '1006', 'L01-13-03', 'L01-13-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 10:52:08', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1597', '0', '10', '700', 'M00351', '1006', 'L04-12-04', 'L04-12-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 10:52:08', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1598', '0', '10', '700', 'M00384', '1006', 'L04-12-05', 'L04-12-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 10:52:08', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1599', '0', '0', '800', 'M00367', '1006', 'L04-14-01', 'L04-11-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 10:58:00', 'superAdmin', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1600', '0', '0', '800', 'S00022', '1006', 'L04-13-03', 'L04-11-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 10:58:00', 'superAdmin', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1601', '0', '10', '700', 'M00384', '1006', 'L04-12-05', 'L04-12-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 11:17:42', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1602', '0', '10', '700', 'M00382', '1006', 'L04-07-03', 'L04-07-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 11:17:42', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1603', '0', '10', '700', 'M00382', '1006', 'L04-07-03', 'L04-07-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 11:31:20', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1604', '0', '10', '700', 'M00385', '1006', 'L04-06-05', 'L04-06-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 11:31:20', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1605', '0', '10', '700', 'M00387', '1006', 'L04-06-04', 'L04-06-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 11:49:38', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1606', '0', '10', '700', 'M00385', '1006', 'L04-06-05', 'L04-06-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 13:15:56', 'xqs', '2019-08-15 17:20:08', 'xqs', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1607', '0', '0', '800', 'M00367', '1006', 'L04-11-05', 'L04-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 14:51:50', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1608', '0', '0', '800', 'S00022', '1006', 'L04-11-04', 'L04-13-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 14:51:50', 'superAdmin', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1609', '0', '5', '100', 'M00401', '0', '', 'L04-06-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 15:15:11', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1610', '0', '5', '100', 'M00400', '0', '', 'L04-06-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 15:15:13', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1611', '0', '10', '300', 'M00400', '1012', 'L04-06-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 15:22:10', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1612', '0', '10', '300', 'M00377', '1012', 'L02-14-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 15:22:11', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1613', '0', '10', '300', 'M00372', '1012', 'L01-14-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 15:27:37', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1614', '0', '10', '700', 'M00387', '1006', 'L04-06-04', 'L04-06-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 15:43:32', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1615', '0', '10', '700', 'M00071', '1006', 'L04-06-01', 'L04-06-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 16:07:55', 'wjj', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1616', '0', '0', '800', 'M00367', '1006', 'L04-14-01', 'L04-11-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 16:10:39', 'Quartz', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1617', '0', '0', '800', 'S00022', '1006', 'L04-13-03', 'L04-11-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 16:10:39', 'Quartz', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1618', '0', '10', '700', 'M00015', '1006', 'L01-14-02', 'L01-14-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 16:22:43', 'wjj', '2019-08-15 17:20:08', 'tony', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1619', '0', '10', '700', 'M00386', '1006', 'L04-08-04', 'L04-08-04', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 16:33:45', 'my test', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1620', '0', '0', '800', 'M00367', '1006', 'L04-11-05', 'L04-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 16:38:02', 'Quartz', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1621', '0', '0', '800', 'S00022', '1006', 'L04-11-04', 'L04-13-03', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-25 16:38:10', 'Quartz', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1622', '0', '5', '100', 'M00396', '0', '', 'L04-05-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 11:25:05', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1623', '0', '5', '100', 'M00400', '0', '', 'L04-05-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 11:25:09', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1624', '0', '5', '100', 'M00399', '0', '', 'L04-05-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 11:25:09', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1625', '0', '10', '300', 'M00019', '1001', 'L01-11-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 11:54:58', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1626', '0', '10', '300', 'M00401', '1012', 'L04-06-02', '', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 11:54:59', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1627', '0', '10', '300', 'M00020', '1006', 'L01-12-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 12:00:03', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1628', '0', '10', '300', 'M00004', '1001', 'L01-12-02', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 13:34:28', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1629', '0', '10', '300', 'M00399', '1012', 'L04-05-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 13:36:45', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1630', '0', '5', '100', 'M00399', '0', '', 'L04-06-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 13:41:08', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1631', '0', '10', '700', 'M00384', '1006', 'L04-12-05', 'L04-12-05', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 13:49:59', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1632', '0', '0', '800', 'M00367', '1006', 'L04-14-01', 'L04-11-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 13:51:03', 'Quartz', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1633', '0', '0', '800', 'M00367', '1006', 'L04-11-05', 'L04-14-01', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 14:05:02', 'Quartz', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1634', '0', '10', '300', 'M00123', '1001', 'L01-12-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 14:18:15', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1635', '0', '10', '300', 'M00110', '1012', 'L01-12-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 14:21:26', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1636', '0', '10', '700', 'M00382', '1006', 'L04-07-03', 'L04-07-03', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-26 16:15:37', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1637', '0', '5', '100', 'M00401', '0', '', 'L04-07-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-27 08:50:44', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1638', '0', '5', '100', 'M00393', '0', '', 'L04-07-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-27 08:50:45', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1639', '0', '10', '300', 'M00401', '1012', 'L04-07-02', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-27 08:55:00', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1640', '0', '10', '600', 'M00352', '1006', 'L04-12-03', null, '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-27 08:57:13', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1641', '0', '10', '600', 'M00339', '1006', 'L04-12-02', null, '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-27 08:59:40', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1642', '0', '10', '300', 'M00015', '1012', 'L01-14-02', '', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-27 09:10:07', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1643', '0', '10', '300', 'M00016', '1012', 'L01-14-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-27 11:08:57', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1644', '0', '10', '600', 'M00375', '1006', 'L04-08-05', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-27 11:33:54', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1645', '0', '5', '100', 'M00401', '0', '', 'L01-01-35-35', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-29 17:14:17', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1646', '0', '10', '300', 'M00031', '1006', 'L04-11-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-29 17:17:07', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1647', '0', '10', '700', 'M00399', '1006', 'L04-06-03', 'L04-06-03', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-29 17:26:37', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1648', '0', '10', '700', 'M00071', '1006', 'L04-06-01', 'L04-06-01', '10', '1', null, '0', '0', null, '0', 'XT0001', '2018-12-29 17:27:08', 'wjj', '2019-08-15 17:20:08', 'Quartz', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1649', '0', '5', '100', 'M00335', '0', '', 'L01-03-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-02 08:52:38', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1650', '0', '5', '100', 'M00336', '0', '', 'L01-01-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-02 08:52:39', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1651', '0', '10', '300', 'S00019', '1006', 'L04-09-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-02 09:00:15', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1652', '0', '10', '700', 'M00336', '1006', 'L01-01-03', 'L01-01-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-02 09:52:15', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1653', '0', '10', '700', 'M00335', '1006', 'L01-03-04', 'L01-03-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-02 09:52:17', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1654', '0', '10', '600', 'M00397', '1006', 'L04-07-04', null, '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-02 09:57:37', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1655', '0', '5', '100', 'M00376', '0', '', 'L01-01-76-76', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 16:22:26', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1656', '0', '5', '100', 'S00019', '0', '', 'L01-01-74-74', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 16:22:27', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1657', '0', '10', '300', 'M00376', '1006', 'L01-01-76-76', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 16:25:03', 'wjj', '2019-08-15 17:20:08', 'wjj', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1658', '0', '10', '700', 'M00396', '1006', 'L04-05-02', 'L04-05-02', '1', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 16:27:55', 'wjj', '2019-08-15 17:20:08', null, '\0');
INSERT INTO `wcstask_deleted` VALUES ('1659', '0', '10', '100', 'M00004', '1006', '', 'L01-14-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 17:17:50', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1660', '0', '10', '100', 'M00005', '1006', '', 'L01-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 17:18:15', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1661', '0', '10', '100', 'M00006', '1006', '', 'L01-12-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 17:18:29', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1662', '0', '10', '100', 'M00007', '1006', '', 'L01-12-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 17:18:48', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1663', '0', '10', '100', 'M00008', '1006', '', 'L01-12-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 17:18:59', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1664', '0', '10', '100', 'M00009', '1006', '', 'L04-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 17:19:09', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1665', '0', '10', '100', 'M00010', '1006', '', 'L01-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 17:19:19', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1666', '0', '10', '100', 'M00011', '1006', '', 'L01-11-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-03 17:19:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1667', '0', '10', '100', 'M00012', '1006', '', 'L02-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 09:10:25', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1668', '0', '10', '100', 'M00013', '1006', '', 'L02-13-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 14:02:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1669', '0', '10', '100', 'M00014', '1006', '', 'L02-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 14:56:27', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1670', '0', '10', '100', 'M00016', '1006', '', 'L02-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 14:57:10', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1671', '0', '10', '300', 'M00012', '1006', 'L02-14-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:18:55', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1672', '0', '10', '300', 'M00013', '1006', 'L02-13-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:19:41', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1673', '0', '10', '300', 'M00014', '1006', 'L02-12-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:20:39', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1674', '0', '10', '300', 'M00016', '1006', 'L02-11-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:20:57', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1675', '0', '10', '300', 'M00004', '1006', 'L01-14-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:21:10', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1676', '0', '10', '300', 'M00005', '1006', 'L01-12-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:21:21', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1677', '0', '10', '300', 'M00006', '1006', 'L01-12-02', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:21:47', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1678', '0', '10', '300', 'M00007', '1006', 'L01-12-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:22:10', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1679', '0', '10', '300', 'M00008', '1006', 'L01-12-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:22:32', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1680', '0', '10', '300', 'M00009', '1006', 'L04-11-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:22:47', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1681', '0', '10', '300', 'M00010', '1006', 'L01-11-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:23:00', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1682', '0', '10', '300', 'M00011', '1006', 'L01-11-02', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:23:24', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1683', '0', '10', '100', 'M00020', '1006', '', 'L01-14-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:36:05', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1684', '0', '10', '100', 'M00021', '1006', '', 'L01-12-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:36:30', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1685', '0', '10', '300', 'M00020', '1006', 'L01-14-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:37:17', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1686', '0', '10', '100', 'M00022', '1006', '', 'L01-14-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:38:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1687', '0', '10', '100', 'M00023', '1006', '', 'L01-12-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:39:00', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1688', '0', '10', '100', 'M00025', '1006', '', 'L01-12-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:39:11', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1689', '0', '10', '100', 'M00026', '1006', '', 'L01-12-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:39:28', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1690', '0', '10', '100', 'M00027', '1006', '', 'L04-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:39:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1691', '0', '10', '100', 'M00028', '1006', '', 'L01-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:40:01', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1692', '0', '10', '100', 'M00029', '1006', '', 'L01-11-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:40:13', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1693', '0', '10', '100', 'M00031', '1006', '', 'L01-11-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:40:30', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1694', '0', '10', '100', 'M00032', '1006', '', 'L01-11-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:40:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1695', '0', '10', '100', 'M00034', '1006', '', 'L01-11-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:41:00', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1696', '0', '10', '100', 'M00037', '1006', '', 'L04-10-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:41:28', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1697', '0', '10', '100', 'M00038', '1006', '', 'L01-10-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:41:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1698', '0', '10', '100', 'M00039', '1006', '', 'L01-10-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:41:50', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1699', '0', '10', '100', 'M00040', '1006', '', 'L01-10-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:02', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1700', '0', '10', '100', 'M00041', '1006', '', 'L01-10-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:13', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1701', '0', '10', '100', 'M00042', '1006', '', 'L04-09-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:22', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1702', '0', '10', '100', 'M00043', '1006', '', 'L04-09-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:34', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1703', '0', '10', '100', 'M00044', '1006', '', 'L01-09-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:42:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1704', '0', '10', '100', 'M00045', '1006', '', 'L01-09-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:48:22', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1705', '0', '10', '100', 'M00047', '1006', '', 'L01-09-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:48:33', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1706', '0', '10', '100', 'M00048', '1006', '', 'L01-09-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:48:43', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1707', '0', '10', '100', 'M00049', '1006', '', 'L01-09-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 15:48:52', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1708', '0', '10', '100', 'M00050', '1006', '', 'L02-14-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 16:40:57', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1709', '0', '10', '100', 'M00051', '1006', '', 'L02-09-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 16:49:35', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1710', '0', '10', '100', 'M00052', '1006', '', 'L02-09-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 16:49:58', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1711', '0', '10', '300', 'M00025', '1006', 'L01-12-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 17:12:28', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1712', '0', '10', '300', 'M00026', '1006', 'L01-12-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 17:13:18', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1713', '0', '10', '300', 'M00027', '1006', 'L04-11-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 17:20:04', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1714', '0', '10', '100', 'M00061', '1006', '', 'L01-12-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 17:21:15', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1715', '0', '10', '100', 'M00062', '1006', '', 'L01-12-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 17:21:25', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1716', '0', '10', '100', 'M00063', '1006', '', 'L04-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 17:21:42', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1717', '0', '10', '300', 'M00022', '1006', 'L01-14-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:11:51', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1718', '0', '15', '800', 'M00050', '1006', 'L02-14-05', 'L02-14-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:11:51', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1719', '0', '10', '300', 'M00028', '1006', 'L01-11-01', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:12:15', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1720', '0', '10', '300', 'M00029', '1006', 'L01-11-02', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:13:18', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1721', '0', '10', '300', 'M00031', '1006', 'L01-11-03', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:13:47', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1722', '0', '10', '100', 'M00072', '1006', '', 'L01-14-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:14:30', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1723', '0', '10', '100', 'M00073', '1006', '', 'L01-11-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:14:39', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1724', '0', '10', '100', 'M00074', '1006', '', 'L01-11-02', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:14:48', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1725', '0', '10', '100', 'M00075', '1006', '', 'L01-11-03', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:14:59', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1726', '0', '10', '100', 'M00076', '1006', '', 'L04-08-01', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:15:08', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1727', '0', '10', '300', 'M00032', '1006', 'L01-11-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:24:52', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1728', '0', '10', '300', 'M00034', '1006', 'L01-11-05', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:49:49', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1729', '0', '10', '300', 'M00037', '1006', 'L04-10-04', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:53:41', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1730', '0', '10', '300', 'M00038', '1006', 'L01-10-02', '', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:55:27', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1731', '0', '10', '100', 'M00081', '1006', '', 'L01-11-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:58:17', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1732', '0', '10', '100', 'M00082', '1006', '', 'L01-11-05', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:58:26', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1733', '0', '10', '100', 'M00083', '1006', '', 'L04-10-04', '100', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 18:58:38', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1734', '0', '10', '300', 'M00039', '1006', 'L01-10-03', '', '10', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 19:00:46', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1735', '0', '10', '300', 'M00040', '1006', 'L01-10-04', '', '10', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 19:07:34', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1736', '0', '10', '300', 'M00041', '1006', 'L01-10-05', '', '10', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 19:09:48', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1737', '0', '10', '300', 'M00042', '1006', 'L04-09-01', '', '10', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 19:12:07', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');
INSERT INTO `wcstask_deleted` VALUES ('1738', '0', '10', '300', 'M00043', '1006', 'L04-09-05', '', '10', '1', null, '0', '0', null, '0', 'XT0001', '2019-01-04 19:12:44', 'youjie', '2019-08-15 17:20:08', 'youjie', '\0');

-- ----------------------------
-- Table structure for wcsuser
-- ----------------------------
DROP TABLE IF EXISTS `wcsuser`;
CREATE TABLE `wcsuser` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userCode` varchar(50) COLLATE utf8_bin NOT NULL,
  `userName` varchar(50) COLLATE utf8_bin NOT NULL,
  `password` varchar(50) COLLATE utf8_bin NOT NULL,
  `partment` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `address` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `phone` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `remark` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `disable` tinyint(4) DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `createdBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `updatedBy` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_user` (`userCode`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsuser
-- ----------------------------
INSERT INTO `wcsuser` VALUES ('16', 'admin', '管理员', '123456', null, null, null, null, '0', null, null, null, null);
INSERT INTO `wcsuser` VALUES ('25', 'Operation', '操作员', '123456', null, null, null, null, '0', null, null, null, null);
INSERT INTO `wcsuser` VALUES ('27', 'huhai', '胡海', '123456', null, null, null, null, '0', null, null, null, null);

-- ----------------------------
-- Table structure for wcsuserrole
-- ----------------------------
DROP TABLE IF EXISTS `wcsuserrole`;
CREATE TABLE `wcsuserrole` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userId` int(11) DEFAULT NULL,
  `roleId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of wcsuserrole
-- ----------------------------
INSERT INTO `wcsuserrole` VALUES ('9', '16', '1');
INSERT INTO `wcsuserrole` VALUES ('10', '16', '2');
INSERT INTO `wcsuserrole` VALUES ('12', '26', '2');
INSERT INTO `wcsuserrole` VALUES ('18', '27', '1');
INSERT INTO `wcsuserrole` VALUES ('19', '25', '2');
